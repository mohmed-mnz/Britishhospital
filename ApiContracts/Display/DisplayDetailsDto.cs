using ApiContracts.DisplayAdvert;
using ApiContracts.DisplayCounter;

namespace ApiContracts.Display;

public class DisplayDetailsDto
{
    public int DisplayId { get; set; }
    public string DisplayName { get; set; } = null!;
    public int? Orgid { get; set; }
    public bool? IsActive { get; set; }
    public string? OrgName { get; set; }
    public List<DisplayAdvertsTinyDto> DisplayAdverts { get; set; } = new List<DisplayAdvertsTinyDto>();
    public List<DisplayCountersTinyDto> DisplayCounters { get; set; } = new List<DisplayCountersTinyDto>();
}
