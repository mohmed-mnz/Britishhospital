using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class ReservationsAddDto
{
    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }

    [JsonPropertyName("servicesId")]
    public List<int> ServicesId { get; set; } = new List<int>();

    [JsonPropertyName("orgId")]
    public int OrgId { get; set; }
}
