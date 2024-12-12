using ApiContracts;
using ApiContracts.Reservation;
using AutoMapper;
using BussinesLayer.HubConfig;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Medallion.Threading;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedConfig;
using System.Data;
using System.Text;
namespace BussinesLayer.Services;

public class ReservationService(IReservationsRepository _repository, IMapper _mapper
    ,IServiceReservationRepository _serviceReservation,AppConfiguration _appConfig,IHubContext<SignalRConfig> _hubContext
    , ICustomerRepository _citizenRepository, IServiceRepository _servicesrepository, IDistributedLockProvider _synchronizationProvider) : IReservationServices
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
            reservationEntity.Status = Status.Pending.ToString();
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
            var username = _appConfig.SmsSetteings!.UserName;
            var password = _appConfig.SmsSetteings!.Password;
            var sender = _appConfig.SmsSetteings!.api_key;
            var message = $"عميلنا العزيز تم حجز طلبكم بنجاح ورقم الخدمة هو {reservationEntity.TicketNumber}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.epusheg.com/api/v2/send_bulk?username={username}&password={password}&api_key={sender}&message={message}&from=BritishHosp&to={reservation.MobileNumber}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());


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

    public async Task<GResponse<List<ReservationsDto>>> GetReservationsByOrgId(int orgid)
    {
        var reservations =await _repository.Where(x=>x.Orgid==orgid)!
            .Include(x => x.Citizen)
            .Include(x => x.Customer)
            .Include(x => x.Service)
            .Include(x => x.Org)
            .AsSplitQuery()
            .ToListAsync();
        var dto = _mapper.Map<List<ReservationsDto>>(reservations);
        return GResponse<List<ReservationsDto>>.CreateSuccess(dto);
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

            case Status.Completed:
                reservationEntity.Status = Status.Completed.ToString();
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

    public Task<GResponse<ReservationsDto>> CallNextInQueue(CallNextInqueueReq callnextinqueuereq)
    {
        var lockKey = $"ReservationServices.CallNextInQueue";
        var @lock = _synchronizationProvider.AcquireLock(lockKey);
        using (@lock)
        {
            var reservation = _repository.Where(x => x.Orgid == callnextinqueuereq.OrgId && x.Status == Status.Pending.ToString() &&x.ServiceId==callnextinqueuereq.ServiceId)!
                .OrderBy(x => x.QueueSerial)
                .FirstOrDefault();
            if (reservation == null)
            {
                throw new ApplicationException("No reservation in queue");
            }
            reservation.Status = Status.Serving.ToString();
            reservation.CallAt = DateTime.Now;
            reservation.CounterId = callnextinqueuereq.CounterId;
            _repository.Commit();
            var dto = _mapper.Map<ReservationsDto>(reservation);
            _hubContext.Clients.Group(callnextinqueuereq.OrgId.ToString()).SendAsync("CallNextInQueue", dto);
            return Task.FromResult(GResponse<ReservationsDto>.CreateSuccess(dto));
        }
    }


    public enum ReservationType
    {
        WalkIn,
        Online
    }
    public enum Status
    {
        Pending,
        Serving,
        Completed,
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

  
}
