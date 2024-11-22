namespace ApiContracts.Service;

public class ServiceUpdateDto
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public string? Prefix { get; set; }
}
