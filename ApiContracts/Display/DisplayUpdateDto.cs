using System.Text.Json.Serialization;

namespace ApiContracts.Display;

public class DisplayUpdateDto
{
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; }
}
