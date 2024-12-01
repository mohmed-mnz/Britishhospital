using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class ReservationsAddDto
{
    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }

    [JsonPropertyName("servicesId")]
    public List<double> ServicesId { get; set; } = new List<double>();
}
