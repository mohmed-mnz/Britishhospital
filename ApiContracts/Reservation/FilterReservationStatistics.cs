namespace ApiContracts.Reservation;

public class FilterReservationStatistics
{
    public int OrgId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}
