using System.Text.Json.Serialization;

namespace ApiContracts.DisplayCounter;

public class DisplayCountersDto
{
    [JsonPropertyName("displayId")]
    public int? DisplayId { get; set; }
    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = null!;
    [JsonPropertyName("counterName")]
    public string CounterName { get; set; } = null!;
}
