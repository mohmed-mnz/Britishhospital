namespace ApiContracts.DisplayAdvert;

public class DisplayAdvertsTinyDto
{
    public int Id { get; set; }
    public int AdvertId { get; set; }
    public string AdvertName { get; set; } = null!;

    public string? Mediatype { get; set; }

    public string? MediaFile { get; set; }

}
