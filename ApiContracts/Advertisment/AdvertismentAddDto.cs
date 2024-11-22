namespace ApiContracts.Advertisment;

public class AdvertismentAddDto
{
    public string AdvertName { get; set; } = null!;
    public string? Mediatype { get; set; }
    public string? MediaFile { get; set; }
}
