namespace ApiContracts.DisplayCounter;

public class DisplayCountersTinyDto
{
    public int? CounterId { get; set; }
    public int Id { get; set; }
    public string CounterName { get; set; } = null!;
}
