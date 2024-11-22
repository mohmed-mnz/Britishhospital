namespace ApiContracts.Display;

public class DisplayUpdateDto
{
    public int DisplayId { get; set; }
    public string? DisplayName { get; set; }
    public int? Orgid { get; set; }
    public bool? IsActive { get; set; }
}
