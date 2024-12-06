namespace Models.Models;

public class ServiceReservation
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public long ReservationId { get; set; }
    public virtual Service? Service { get; set; }
    public virtual Reservations? Reservation { get; set; }

}
