using System.Text.Json.Serialization;

namespace ApiContracts.Advertisment;

public class AdvertismentAddDto
{
    [JsonPropertyName("advertName")]
    public string AdvertName { get; set; } = null!;
    [JsonPropertyName("mediaType")]
    public string? Mediatype { get; set; }
    [JsonPropertyName("mediaFile")]
    public string? MediaFile { get; set; }
}
