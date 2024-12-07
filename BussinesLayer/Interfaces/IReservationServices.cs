using ApiContracts;
using ApiContracts.Reservation;

namespace BussinesLayer.Interfaces;

public interface IReservationServices
{
    Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation);
    Task<GResponse<ReservationsDto>> GetReservation(long id);
    Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsUpdateStatusDto reservation);
    Task<GResponse<ReservationsDto>> CancellReservation(long id);
    Task<GResponse<List<ReservationsDto>>> GetReservationsByOrgId(int orgId);
}
