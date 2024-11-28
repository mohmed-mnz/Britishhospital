using System.Text.Json.Serialization;

namespace ApiContracts.DisplayAdvert;

public class DisplayAdvertsUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("advertId")]
    public int AdvertId { get; set; }
    [JsonPropertyName("displayId")]
    public int DisplayId { get; set; }

}
