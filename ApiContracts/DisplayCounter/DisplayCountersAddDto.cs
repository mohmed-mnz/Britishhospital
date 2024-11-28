using System.Text.Json.Serialization;

namespace ApiContracts.DisplayCounter;

public class DisplayCountersAddDto
{
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
    [JsonPropertyName("counterId")]
    public int CounterId { get; set; }
}
