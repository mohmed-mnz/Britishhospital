using System.Text.Json.Serialization;

namespace ApiContracts.CounterService;

public class CounterServicesDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }
    [JsonPropertyName("serviceId")]
    public int? ServiceId { get; set; }
    [JsonPropertyName("serviceName")]
    public string? serviceName { get; set; }
    [JsonPropertyName("counterName")]
    public string? CounterName { get; set; }
}
