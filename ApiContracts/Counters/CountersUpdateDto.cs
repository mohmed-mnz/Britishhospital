namespace ApiContracts.Counters;

public class CountersUpdateDto
{
    public int CounterId { get; set; }
    public string? CounterName { get; set; }
    public bool? IsActive { get; set; }
    public int empid { get; set; }
}
