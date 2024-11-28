using System.Text.Json.Serialization;

namespace ApiContracts.Advertisment;

public class AdvertismentUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("advertName")]
    public string AdvertName { get; set; } = null!;
    [JsonPropertyName("mediaType")]
    public string? Mediatype { get; set; }
    [JsonPropertyName("mediaFile")]
    public string? MediaFile { get; set; }
}
