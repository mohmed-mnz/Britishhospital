using ApiContracts;
using ApiContracts.CounterService;
using ApiContracts.Reservation;
using AutoMapper;
using BussinesLayer.HubConfig;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Medallion.Threading;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedConfig;
using System.Data;
using System.Security.Cryptography;
using System.Text;
namespace BussinesLayer.Services;

public class ReservationService(IReservationsRepository _repository, IMapper _mapper
    ,IServiceReservationRepository _serviceReservation,AppConfiguration _appConfig,IHubContext<SignalRConfig> _hubContext
    , ICustomerRepository _citizenRepository, IServiceRepository _servicesrepository, IDistributedLockProvider _synchronizationProvider,ICounterServicesRepository _counterServicesRepository) 
    : IReservationServices
{
    public async Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation)
    {
        var lockKey = $"ReservationServices.AddReservation";
        var @lock = await _synchronizationProvider.AcquireLockAsync(lockKey);
        await using (@lock)
        {
            var reservationEntity = _mapper.Map<Reservations>(reservation);
            reservationEntity.IsCancelled = false;
            reservationEntity.ReservationDate = DateTime.Now;
            reservationEntity.Status = Status.Waiting.ToString();
            var citizen = await _citizenRepository.Where(x => x.Citizen!.Mobile == reservation.MobileNumber)!
                .Include(x => x.Citizen)
                .FirstOrDefaultAsync();

            reservationEntity.CitizenId = citizen == null ? null : citizen.Citizen!.CitizenId;
            reservationEntity.CustomerId = citizen == null ? null : citizen.Id;
            reservationEntity.Name = citizen == null ? "" : citizen.Citizen!.Name;
            reservationEntity.MobileNumber = reservation.MobileNumber;
            reservationEntity.ReservationType = ReservationType.WalkIn.ToString();
            reservationEntity.Orgid = reservation.OrgId;
            reservationEntity.EndServing = null;

          
            await Task.WhenAll(_repository.InsertAsync(reservationEntity), _repository.Commit());
            if (reservation.ServicesId.Count > 1)
            {
                var service = await _servicesrepository.Where(x => x.OrgId == reservation.OrgId && x.Id == 1)!.FirstOrDefaultAsync();
                reservationEntity.ServiceId = service!.Id;

                var Queueserial = await GenerateCounterForServicesAsync(DateTime.Now, reservation.OrgId, reservationEntity.ServiceId ?? 0);
                reservationEntity.QueueSerial = Queueserial;

                var serial = "";
                if (int.TryParse(service.Prefix, out int numericPrefix))
                {
                    serial = $"{numericPrefix + Queueserial}";
                }
                else
                {
                    service.Prefix = string.IsNullOrEmpty(service.Prefix) ? "0" : service.Prefix;
                    serial = $"{service.Prefix} - {(Queueserial)}";
                }
                reservationEntity.TicketNumber = serial;


                var servicesreservations = new List<ServiceReservation>();
                foreach (var item in reservation.ServicesId)
                {
                    servicesreservations.Add(new ServiceReservation
                    {
                        ServiceId = item,
                        ReservationId = reservationEntity.Id
                    });
                }
                await _serviceReservation.InsertRangeAsync(servicesreservations);
            }
            else
            {
                reservationEntity.ServiceId = reservation.ServicesId[0];
                var service = await _servicesrepository.Where(x => x.OrgId == reservation.OrgId && x.Id == reservationEntity.ServiceId)!.FirstOrDefaultAsync();
                var Queueserial = await GenerateCounterForServicesAsync(DateTime.Now, reservation.OrgId, reservationEntity.ServiceId ?? 0);
                reservationEntity.QueueSerial = Queueserial;
                var serial = "";
                if (int.TryParse(service!.Prefix, out int numericPrefix))
                {
                    serial = $"{numericPrefix + Queueserial}";
                }
                else
                {
                    service.Prefix = string.IsNullOrEmpty(service.Prefix) ? "0" : service.Prefix;
                    serial = $"{service.Prefix} - {(Queueserial)}";
                }
                reservationEntity.TicketNumber = serial;
                var servicesreservations = new List<ServiceReservation>
            {
                new ServiceReservation
                {
                    ServiceId = reservationEntity.ServiceId??0,
                    ReservationId = reservationEntity.Id
                }
            };
                await _serviceReservation.InsertRangeAsync(servicesreservations);
            }
            var dto = _mapper.Map<ReservationsDto>(reservationEntity);
            await _hubContext.Clients.Group(reservation.OrgId.ToString()).SendAsync("AddReservation", dto);
          //  await _hubContext.Clients.All.SendAsync("AddReservation", dto);
            var username = _appConfig.SmsSetteings!.UserName;
            var password = _appConfig.SmsSetteings!.Password;
            var sender = _appConfig.SmsSetteings!.api_key;
            var message = $"عميلنا العزيز تم حجز طلبكم بنجاح ورقم الخدمة هو {reservationEntity.TicketNumber}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.epusheg.com/api/v2/send_bulk?username={username}&password={password}&api_key={sender}&message={message}&from=BritishHosp&to={reservation.MobileNumber}");
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch
            (Exception)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            return GResponse<ReservationsDto>.CreateSuccess(dto);
        }
    }



    public async Task<GResponse<ReservationsDto>> CancellReservation(long id)
    {
        var reservation =await _repository.Where(x => x.Id == id)!.FirstOrDefaultAsync();
        reservation=reservation== null ? throw new Exception("Reservation not found") : reservation;
        reservation.IsCancelled = true;
        reservation.Status = Status.Cancelled.ToString();
        await Task.WhenAll( _repository.Commit());
        var dto = _mapper.Map<ReservationsDto>(reservation);
        await _hubContext.Clients.Group(reservation.Orgid.ToString()).SendAsync("CancellReservation", dto);
        return GResponse<ReservationsDto>.CreateSuccess(dto);
    }

    public async Task<GResponse<ReservationsDto>> GetReservation(long id)
    {
        var reservation = await _repository.Where(x => x.Id == id)!.FirstOrDefaultAsync();
        reservation = reservation == null ? throw new Exception("Reservation not found") : reservation;
        var dto = _mapper.Map<ReservationsDto>(reservation);
        return GResponse<ReservationsDto>.CreateSuccess(dto);
    }

    public async Task<GResponse<List<ReservationsCounterDetailsDto>>> GetReservationsByOrgId(int orgid, int counterid)
    {
        var servicesId = await _counterServicesRepository
            .Where(x => x.CounterId == counterid)!
            .Select(x => x.ServiceId)
            .ToListAsync();

        var reservations = await _repository
            .Where(x => x.Orgid == orgid && servicesId.Contains(x.ServiceId) && x.ReservationDate.Date == DateTime.Now.Date)!
            .Select(r => new
            {
                r.Id,
                r.Nid,
                r.ReservationDate,
                r.QueueSerial,
                r.CitizenId,
                r.CustomerId,
                r.Status,
                r.MobileNumber,
                r.TicketNumber,
                r.ServiceId,
                r.CallAt,
                r.ReservationType,
                r.Orgid,
                r.Org!.OrgName,
                r.Service!.ServiceName,
                CounterName = r.Service.CounterServices.Select(cs => cs.Counter!.CounterName).FirstOrDefault(),
                CounterId = r.Service.CounterServices.Select(cs => cs.CounterId).FirstOrDefault(),
                Services = r.ReservationsServices!.Select(rs => rs.Service!.ServiceName).ToList()
            })
            .ToListAsync();

        var groupedReservations = reservations
            .GroupBy(r => r.CounterName)
            .Select(group => new ReservationsCounterDetailsDto
            {
                CounterName = group.Key,
                ReservationData = group.Select(r => new ReservationsDto
                {
                    Id = r.Id,
                    Nid = r.Nid!,
                    ReservationDate = r.ReservationDate,
                    CreatedOn = DateTime.Now,
                    QueueSerial = r.QueueSerial,
                    CitizenId = r.CitizenId ?? 0,
                    CustomerId = r.CustomerId ?? 0,
                    Status = r.Status,
                    CounterId = r.CounterId,
                    MobileNumber = r.MobileNumber,
                    TicketNumber = r.TicketNumber,
                    ReqId = r.ServiceId,
                    CallAt = r.CallAt,
                    ReservationType = r.ReservationType,
                    Orgid = r.Orgid,
                    OrgName = r.OrgName,
                    Services = r.Services!,
                    EndServing = null
                }).ToList(),
                dtos = group
                    .Select(r => new CounterServicesTinyDto
                    {
                        ServiceId = r.ServiceId ?? 0,
                        serviceName = r.ServiceName,
                    })
                    .DistinctBy(dto => dto.ServiceId) 
                    .ToList()
            })
            .ToList();

        return GResponse<List<ReservationsCounterDetailsDto>>.CreateSuccess(groupedReservations);
    }

    public async Task<GResponse<List<ReservationsCounterDetailsDto>>> GetReservationsServingByOrgId(int orgid, int counterid)
    {
        var servicesId = await _counterServicesRepository
            .Where(x => x.CounterId == counterid)!
            .Select(x => x.ServiceId)
            .ToListAsync();

        var reservations = await _repository
            .Where(x => x.Orgid == orgid && servicesId.Contains(x.ServiceId) && x.ReservationDate.Date == DateTime.Now.Date && x.Status==Status.Serving.ToString())!
            .Select(r => new
            {
                r.Id,
                r.Nid,
                r.ReservationDate,
                r.QueueSerial,
                r.CitizenId,
                r.CustomerId,
                r.Status,
                r.MobileNumber,
                r.TicketNumber,
                r.ServiceId,
                r.CallAt,
                r.ReservationType,
                r.Orgid,
                r.Org!.OrgName,
                r.Service!.ServiceName,
                CounterName = r.Service.CounterServices.Select(cs => cs.Counter!.CounterName).FirstOrDefault(),
                CounterId = r.Service.CounterServices.Select(cs => cs.CounterId).FirstOrDefault(),
                Services = r.ReservationsServices!.Select(rs => rs.Service!.ServiceName).ToList()
            })
            .ToListAsync();

        var groupedReservations = reservations
            .GroupBy(r => r.CounterName)
            .Select(group => new ReservationsCounterDetailsDto
            {
                CounterName = group.Key,
                ReservationData = group.Select(r => new ReservationsDto
                {
                    Id = r.Id,
                    Nid = r.Nid!,
                    ReservationDate = r.ReservationDate,
                    CreatedOn = DateTime.Now,
                    QueueSerial = r.QueueSerial,
                    CitizenId = r.CitizenId ?? 0,
                    CustomerId = r.CustomerId ?? 0,
                    Status = r.Status,
                    CounterId = r.CounterId,
                    MobileNumber = r.MobileNumber,
                    TicketNumber = r.TicketNumber,
                    ReqId = r.ServiceId,
                    CallAt = r.CallAt,
                    ReservationType = r.ReservationType,
                    Orgid = r.Orgid,
                    OrgName = r.OrgName,
                    Services = r.Services!,
                    EndServing = null
                }).ToList(),
                dtos = group
                    .Select(r => new CounterServicesTinyDto
                    {
                        ServiceId = r.ServiceId ?? 0,
                        serviceName = r.ServiceName,
                    })
                    .DistinctBy(dto => dto.ServiceId)
                    .ToList()
            })
            .ToList();

        return GResponse<List<ReservationsCounterDetailsDto>>.CreateSuccess(groupedReservations);
    }



    public async Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsUpdateStatusDto reservation)
    {
        var lockKey = $"ReservationServices.AddReservation";
        var @lock = await _synchronizationProvider.AcquireLockAsync(lockKey);
        await using (@lock)
        {
            if (!Enum.IsDefined(typeof(Status), reservation.Status))
            {
                throw new ArgumentException("Invalid reservation status", nameof(reservation.Status));
            }

            var reservationEntity = await _repository.Where(x => x.Id == reservation.Id)!.FirstOrDefaultAsync();
            if (reservationEntity == null)
            {
                throw new ApplicationException("Reservation not found");
            }

            reservationEntity = _mapper.Map(reservation, reservationEntity);


            reservationEntity = UpdateReservationStatus(reservation.Status, reservationEntity);

            await _repository.Commit();

            //await _hubContext.Clients.All
            //    .SendAsync("UpdateReservation", reservationEntity);


            await _hubContext.Clients.Group(reservationEntity.Orgid.ToString())
            .SendAsync("UpdateReservation", reservationEntity);

            var dto = _mapper.Map<ReservationsDto>(reservationEntity);
            return GResponse<ReservationsDto>.CreateSuccess(dto);
        }
    }

    private Reservations UpdateReservationStatus(int status, Reservations reservationEntity)
    {
        switch ((Status)status)
        {
            case Status.Serving:
                reservationEntity.Status = Status.Serving.ToString();
                reservationEntity.CallAt = DateTime.Now;
                break;

            case Status.Served:
                reservationEntity.Status = Status.Served.ToString();
                reservationEntity.EndServing = DateTime.Now;
                break;

            case Status.Cancelled:
                reservationEntity.Status = Status.Cancelled.ToString();
                reservationEntity.IsCancelled = true;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(status), "Unsupported reservation status");
        }

        return reservationEntity;
    }

    public async Task<GResponse<ReservationsDto>> CallNextInQueue(CallNextInqueueReq callnextinqueuereq)
    {
        var lockKey = $"ReservationServices.CallNextInQueue.Orgid : {callnextinqueuereq.OrgId}";
        var @lock = _synchronizationProvider.AcquireLock(lockKey);
        using (@lock)
        {
            var reservation =await _repository.Where(x => x.Orgid == callnextinqueuereq.OrgId && x.Status == Status.Waiting.ToString() &&x.ServiceId==callnextinqueuereq.ServiceId)!
                .Include(se=>se.Service)
                .OrderBy(x => x.QueueSerial)
                .FirstOrDefaultAsync();
            if (reservation == null)
            {
                throw new ApplicationException("No reservation in queue");
            }
            reservation.Status = Status.Serving.ToString();
            reservation.CallAt = DateTime.Now;
            reservation.CounterId = callnextinqueuereq.CounterId;
            var counterName = await _counterServicesRepository.Where(x => x.CounterId == callnextinqueuereq.CounterId && x.ServiceId == callnextinqueuereq.ServiceId)!
                .Select(x => x.Counter!.CounterName)
                .FirstOrDefaultAsync();




            await _repository.Commit();
            var dto = _mapper.Map<ReservationsDto>(reservation);

                dto.Services = _repository.Where(x => x.Id == dto.Id)!.Select(x => x.ReservationsServices!.Select(x => x.Service!.ServiceName).ToList()).FirstOrDefault()!;
            






            //    await _hubContext.Clients.All.SendAsync("CallNextInQueue", dto);
            await 
                _hubContext.Clients.Group(callnextinqueuereq.OrgId.ToString())
                .SendAsync("CallNextInQueue", dto);

            var username = _appConfig.SmsSetteings!.UserName;
            var password = _appConfig.SmsSetteings!.Password;
            var sender = _appConfig.SmsSetteings!.api_key;
            var message = $"عميلنا العزيز تم استدعائك للدخول للخدمة الخاصة بك في {counterName}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.epusheg.com/api/v2/send_bulk?username={username}&password={password}&api_key={sender}&message={message}&from=BritishHosp&to={reservation.MobileNumber}");
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch
            (Exception)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            return GResponse<ReservationsDto>.CreateSuccess(dto);
        }
    }


    public enum ReservationType
    {
        WalkIn,
        Online
    }
    public enum Status
    {
        Waiting,
        Serving,
        Served,
        Cancelled
    }
    public async Task<int> GenerateCounterForServicesAsync(DateTime reservationDate, int orgId, int qgId)
    {
        using var connection = new SqlConnection(_appConfig.DbConfig!.BritshHospitalConnctionString);
        using var command = new SqlCommand("GenerateCounterForServices", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@ReservationDate", reservationDate.Date);
        command.Parameters.AddWithValue("@OrgId", orgId);
        command.Parameters.AddWithValue("@QGID", qgId);

        var counterOutput = new SqlParameter("@Counter", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(counterOutput);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        return (int)counterOutput.Value;
    }

    public async Task<GResponse<ReservationStatisticsDto>> reservationstatisticsbasedonorgid(FilterReservationStatistics filter)
    {
        var reservation =await _repository.Where(x => x.Orgid == filter.OrgId && x.ReservationDate.Date >= filter.FromDate.Date && x.ReservationDate.Date <= filter.ToDate.Date)!
            .Include(x => x.Service)
            .Include(x => x.Org)
            .AsSplitQuery()
            .ToListAsync();
        var dto = new ReservationStatisticsDto
        {
            totalCancelledReservations = reservation.Count(x => x.Status == Status.Cancelled.ToString()),
            totalCompletedReservations = reservation.Count(x => x.Status == Status.Served.ToString()),
            totalPendingReservations = reservation.Count(x => x.Status == Status.Waiting.ToString()),
            totalServingReservations = reservation.Count(x => x.Status == Status.Serving.ToString()),
            TotalReservations = reservation.Count()
        };
        return GResponse<ReservationStatisticsDto>.CreateSuccess(dto);
    }

    public async Task<GResponse<List<ReservationsDto>>> GetReservationsBasedOnOrgId(int orgId)
    {
        var reservations = await _repository
           .Where(x => x.Orgid == orgId && x.ReservationDate.Date==DateTime.Now.Date)!
           .Include(x => x.Service)
               .ThenInclude(s => s.CounterServices)
                   .ThenInclude(cs => cs.Counter)
           .Include(x => x.Citizen)
           .Include(x => x.Customer)
           .Include(x => x.Org)
           .AsSplitQuery()
           .ToListAsync();

        var dto = reservations.Select(r => _mapper.Map<ReservationsDto>(r)).ToList();
        return GResponse<List<ReservationsDto>>.CreateSuccess(dto);

    }

    public async Task<GResponse<List<ReservationsDto>>> GetServingReservationsBasedOnOrgId(int orgId)
    {
        var reservations = await _repository.Where(x => x.Orgid == orgId && x.Status == Status.Serving.ToString()&&x.ReservationDate.Date==DateTime.Now.Date)!
            .Include(x => x.Service)
            .Include(x => x.Citizen)
            .Include(x => x.Customer)
            .Include(x => x.Org)
            .Include(x=>x.ReservationsServices)
            .AsSplitQuery()
            .ToListAsync();
        var dto = reservations.Select(r => _mapper.Map<ReservationsDto>(r)).ToList();
        foreach (var item in dto)
        {
            item.Services = reservations.Where(x => x.Id == item.Id).Select(x => x.ReservationsServices!.Select(x => x.Service!.ServiceName).ToList()).FirstOrDefault()!;
            item.OrgName = reservations.Where(x => x.Id == item.Id).Select(x => x.Org!.OrgName).FirstOrDefault()!;
            item.OrgSerial = reservations.Where(x => x.Id == item.Id).Select(x => x.QueueSerial).FirstOrDefault()!;
            item.QueueSerial = reservations.Where(x => x.Id == item.Id).Select(x => x.QueueSerial).FirstOrDefault()!;
            item.CreatedOn = DateTime.Now;
            item.EndServing = null;
        }
        return GResponse<List<ReservationsDto>>.CreateSuccess(dto);
    }

    public async Task<GResponse<List<AdminReservationStatisticsBasedOneveryDayInTheWeekDto>>> GetReservationStatisticsBasedOnEveryDayInTheWeek(FilterReservationStatistics filterData)
    {
        var query = await _repository
                    .Where(r => r.ReservationDate.Date >= filterData.FromDate.Date
                                && r.ReservationDate.Date <= filterData.ToDate.Date
                                &&r.Orgid == filterData.OrgId)!
                    .GroupBy(r => r.ReservationDate!.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        TotalCount = g.Count(),
                        TotalServedCount = g.Count(x => x.Status == Status.Served.ToString())
                    })
                    .ToListAsync();

        // Map DayOfWeek and aggregate by day
        var groupedByDay = query
            .GroupBy(q => q.Date.DayOfWeek)
            .Select(g => new
            {
                Day = g.Key.ToString(),
                TotalCount = g.Sum(x => x.TotalCount),
                TotalServedCount = g.Sum(x => x.TotalServedCount),
                AverageCount = g.Average(x => x.TotalCount),
                AverageServedCount = g.Average(x => x.TotalServedCount)
            })
            .ToList();

        // Map to the result DTO
        var result = groupedByDay.Select(g => new AdminReservationStatisticsBasedOneveryDayInTheWeekDto
        {
            day = g.Day,
            totalcount = g.TotalCount,
            totalservedcount = g.TotalServedCount,
            averagecount = Math.Round(g.AverageCount, 2),
            averageservedcount = Math.Round(g.AverageServedCount, 2)
        }).ToList();

        return GResponse<List<AdminReservationStatisticsBasedOneveryDayInTheWeekDto>>.CreateSuccess(result);
    }
}
