using System.Text.Json.Serialization;

namespace ApiContracts.Service;

public class ServiceUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("serviceName")]
    public string ServiceName { get; set; } = null!;
    [JsonPropertyName("prefix")]
    public string? Prefix { get; set; }
}
