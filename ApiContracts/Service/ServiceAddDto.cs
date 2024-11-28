using System.Text.Json.Serialization;

namespace ApiContracts.Service;

public class ServiceAddDto
{
    [JsonPropertyName("serviceName")]
    public string ServiceName { get; set; } = null!;
    [JsonPropertyName("prefix")]
    public string? Prefix { get; set; }
}
