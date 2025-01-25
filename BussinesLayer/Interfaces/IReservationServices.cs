using ApiContracts;
using ApiContracts.Reservation;

namespace BussinesLayer.Interfaces;

public interface IReservationServices
{
    Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation);
    Task<GResponse<ReservationsDto>> GetReservation(long id);
    Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsUpdateStatusDto reservation);
    Task<GResponse<ReservationsDto>> CancellReservation(long id);
    Task<GResponse<List<ReservationsCounterDetailsDto>>> GetReservationsByOrgId(int orgid, int counterid, int serviceId);
    Task<GResponse<List<ReservationsDto>>> GetReservationsBasedOnOrgId(int orgId);
    Task<GResponse<List<ReservationsCounterDetailsDto>>> GetReservationsServingByOrgId(int orgid, int counterid);
    Task<GResponse<ReservationsDto>>CallNextInQueue(CallNextInqueueReq callnextinqueuereq);
    Task<GResponse<ReservationStatisticsDto>> reservationstatisticsbasedonorgid(FilterReservationStatistics filter);
    Task<GResponse<List<ReservationsDto>>> GetServingReservationsBasedOnOrgId(int orgId);
    Task<GResponse<List<AdminReservationStatisticsBasedOneveryDayInTheWeekDto>>> GetReservationStatisticsBasedOnEveryDayInTheWeek(FilterReservationStatistics filterData);

}
