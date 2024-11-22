namespace ApiContracts.Display;

public class DisplayDto
{
    public int DisplayId { get; set; }

    public string DisplayName { get; set; } = null!;

    public int? Orgid { get; set; }

    public bool? IsActive { get; set; }
    public string? OrgName { get; set; }
}
