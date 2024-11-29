using System.Text.Json.Serialization;

namespace ApiContracts.CounterService;

public class CounterServicesAddDto
{

    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }
    [JsonPropertyName("serviceId")]
    public int? ServiceId { get; set; }
}
