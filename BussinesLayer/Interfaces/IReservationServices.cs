using ApiContracts;
using ApiContracts.Reservation;

namespace BussinesLayer.Interfaces;

public interface IReservationServices
{
    Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation);
    Task<GResponse<ReservationsDto>> GetReservation(long id);
    Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsUpdateStatusDto reservation);
    Task<GResponse<ReservationsDto>> CancellReservation(long id);
    Task<GResponse<List<ReservationsCounterDetailsDto>>> GetReservationsByOrgId(int orgId,int counterid);
    Task<GResponse<List<ReservationsDto>>> GetReservationsBasedOnOrgId(int orgId);

    Task<GResponse<ReservationsDto>>CallNextInQueue(CallNextInqueueReq callnextinqueuereq);
    Task<GResponse<ReservationStatisticsDto>> reservationstatisticsbasedonorgid(FilterReservationStatistics filter);
    Task<GResponse<List<ReservationsDto>>> GetServingReservationsBasedOnOrgId(int orgId);

}
