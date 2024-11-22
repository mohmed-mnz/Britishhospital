namespace ApiContracts.Advertisment;

public class AdvertismentDto
{
    public int Id { get; set; }
    public string AdvertName { get; set; } = null!;
    public string? Mediatype { get; set; }
    public string? MediaFile { get; set; }
}
