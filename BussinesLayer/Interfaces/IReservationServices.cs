using ApiContracts;
using ApiContracts.Reservation;

namespace BussinesLayer.Interfaces;

public interface IReservationServices
{
    Task<GResponse<ReservationsDto>> AddReservation(ReservationsAddDto reservation);
    Task<GResponse<ReservationsDto>> GetReservation(long id);
    Task<GResponse<ReservationsDto>> UpdateReservation(ReservationsDto reservation);
    Task<GResponse<ReservationsDto>> DeleteReservation(long id);
    Task<GResponse<List<ReservationsDto>>> GetReservations();
    Task<GResponse<List<ReservationsDto>>> GetReservationsByOrgId(int orgId);
}
