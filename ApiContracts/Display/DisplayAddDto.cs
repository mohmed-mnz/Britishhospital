using System.Text.Json.Serialization;

namespace ApiContracts.Display;

public class DisplayAddDto
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = null!;
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
    [JsonPropertyName("isActive")]
    public bool? IsActive { get; set; }
}
