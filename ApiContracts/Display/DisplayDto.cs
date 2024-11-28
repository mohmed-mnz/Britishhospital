using System.Text.Json.Serialization;

namespace ApiContracts.Display;

public class DisplayDto
{
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = null!;
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; }
    [JsonPropertyName("orgName")]
    public string? OrgName { get; set; }
}
