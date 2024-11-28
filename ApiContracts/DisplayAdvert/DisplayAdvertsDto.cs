using System.Text.Json.Serialization;

namespace ApiContracts.DisplayAdvert;

public class DisplayAdvertsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("advertId")]
    public int AdvertId { get; set; }
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
    [JsonPropertyName("advertName")]
    public string AdvertName { get; set; } = null!;
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = null!;

}
