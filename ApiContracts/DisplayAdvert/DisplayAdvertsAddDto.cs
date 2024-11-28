using System.Text.Json.Serialization;

namespace ApiContracts.DisplayAdvert;

public class DisplayAdvertsAddDto
{
    [JsonPropertyName("advertId")]
    public int AdvertId { get; set; }
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }
}
