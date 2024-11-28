using System.Text.Json.Serialization;

namespace ApiContracts.Service;

public class ServiceDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("serviceName")]
    public string? ServiceName { get; set; }
    [JsonPropertyName("prefix")]
    public string? Prefix { get; set; }
}
