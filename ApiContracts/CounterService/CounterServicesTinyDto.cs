using System.Text.Json.Serialization;

namespace ApiContracts.CounterService;

public class CounterServicesTinyDto
{
    [JsonPropertyName("serviceId")]
    public int? ServiceId { get; set; }
    [JsonPropertyName("serviceName")]
    public string? serviceName { get; set; }
}
