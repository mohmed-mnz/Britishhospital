using System.Text.Json.Serialization;

namespace ApiContracts.CounterService;

public class CounterServicesUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }
    [JsonPropertyName("serviceId")]
    public int? ServiceId { get; set; }
}
