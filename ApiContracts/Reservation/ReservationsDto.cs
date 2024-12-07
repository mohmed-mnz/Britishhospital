using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class ReservationsDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("nid")]
    public string Nid { get; set; } = null!;
    [JsonPropertyName("reservationDate")]
    public DateTime ReservationDate { get; set; }
    [JsonPropertyName("createdOn")]
    public DateTime CreatedOn { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("queueSerial")]
    public double? QueueSerial { get; set; }
    [JsonPropertyName("bookingDetails")]
    public string? BookingDetails { get; set; }
    [JsonPropertyName("citizenId")]
    public int CitizenId { get; set; }
    [JsonPropertyName("customerId")]
    public int CustomerId { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }
    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }
    [JsonPropertyName("ticketNumber")]
    public string? TicketNumber { get; set; }
    [JsonPropertyName("servicesId")]
    public double? ReqId { get; set; }
    [JsonPropertyName("callAt")]
    public DateTime? CallAt { get; set; }
    [JsonPropertyName("endServing")]
    public DateTime EndServing { get; set; }
    [JsonPropertyName("reservationType")]
    public string? ReservationType { get; set; }
    [JsonPropertyName("orgSerial")]
    public int? OrgSerial { get; set; }
    [JsonPropertyName("transferedAt")]
    public DateTime? TransferedAt { get; set; }
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = null!;

}
