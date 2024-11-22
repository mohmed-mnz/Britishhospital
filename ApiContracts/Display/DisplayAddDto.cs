namespace ApiContracts.Display;

public class DisplayAddDto
{
    public string DisplayName { get; set; } = null!;
    public int? Orgid { get; set; }
    public bool? IsActive { get; set; }
}
