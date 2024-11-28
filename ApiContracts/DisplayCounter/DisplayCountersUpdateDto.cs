using System.Text.Json.Serialization;

namespace ApiContracts.DisplayCounter;

public class DisplayCountersUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
    [JsonPropertyName("counterId")]
    public int CounterId { get; set; }
}
