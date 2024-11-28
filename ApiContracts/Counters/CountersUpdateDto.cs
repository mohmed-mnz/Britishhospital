using System.Text.Json.Serialization;

namespace ApiContracts.Counters;

public class CountersUpdateDto
{
    [JsonPropertyName("counterId")]
    public int CounterId { get; set; }
    [JsonPropertyName("counterName")]
    public string? CounterName { get; set; }
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; }
    [JsonPropertyName("empId")]
    public int empid { get; set; }
}
