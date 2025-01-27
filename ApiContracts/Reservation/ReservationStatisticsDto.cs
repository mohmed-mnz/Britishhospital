namespace ApiContracts.Reservation;

public class ReservationStatisticsDto
{
    public int TotalReservations { get; set; }
    public int totalPendingReservations { get; set; }
    public int totalServingReservations { get; set; }
    public int totalCompletedReservations{ get; set; }
    public int totalCancelledReservations { get; set; }

}
