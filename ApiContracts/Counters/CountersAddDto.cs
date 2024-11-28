using System.Text.Json.Serialization;

namespace ApiContracts.Counters;

public class CountersAddDto
{
    [JsonPropertyName("counterName")]
    public string? CounterName { get; set; }
    [JsonPropertyName("counterName")]
    public bool? IsActive { get; set; }
    [JsonPropertyName("counterName")]
    public int empid { get; set; }
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
}
