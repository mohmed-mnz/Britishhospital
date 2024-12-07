using ApiContracts;
using ApiContracts.Reservation;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Models.Models;
using SharedConfig;
using System.Data;

namespace BussinesLayer.Services;

public class ReservationService(IReservationsRepository _repository, IMapper _mapper
    ,IServiceReservationRepository _serviceReservation,AppConfiguration _appConfig
    , ICustomerRepository _citizenRepository,IServiceRepository _servicesrepository) : IReservationServices
{
    public async Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation)
    {
        var reservationEntity = _mapper.Map<Reservations>(reservation);
        reservationEntity.IsCancelled = false;
        reservationEntity.ReservationDate = DateTime.Now;
        reservationEntity.Status = Status.Pending.ToString();
        var citizen = await _citizenRepository.Where(x => x.Citizen!.Mobile== reservation.MobileNumber)!
            .Include(x=>x.Citizen)
            .FirstOrDefaultAsync();

        reservationEntity.CitizenId = citizen == null ? 0 : citizen.Citizen!.CitizenId;
        reservationEntity.CustomerId = citizen == null ? 0 : citizen.Id;
        reservationEntity.Name =  citizen == null ? "" : citizen.Citizen!.Name;
        reservationEntity.MobileNumber = reservation.MobileNumber;
        reservationEntity.ReservationType= ReservationType.WalkIn.ToString();
        reservationEntity.Orgid= reservation.OrgId;

        if(reservation.ServicesId.Count > 1)
        {
            var service =await _servicesrepository.Where(x=>x.OrgId==reservation.OrgId && x.Id == 1)!.FirstOrDefaultAsync();
            reservationEntity.ServiceId = service.Id;

            var Queueserial = await GenerateCounterForServicesAsync(DateTime.Now, reservation.OrgId, reservationEntity.ServiceId??0);
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
        }










    }

    public Task<GResponse<ReservationsDto>> DeleteReservation(long id)
    {
        throw new NotImplementedException();
    }

    public Task<GResponse<ReservationsDto>> GetReservation(long id)
    {
        throw new NotImplementedException();
    }

    public Task<GResponse<List<ReservationsDto>>> GetReservations()
    {
        throw new NotImplementedException();
    }

    public Task<GResponse<List<ReservationsDto>>> GetReservationsByOrgId(int orgId)
    {
        throw new NotImplementedException();
    }

    public Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsDto reservation)
    {
        throw new NotImplementedException();
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
