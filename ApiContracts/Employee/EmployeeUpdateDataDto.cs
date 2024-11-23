namespace ApiContracts.Employee;

public class EmployeeUpdateDataDto
{
    public int Id { get; set; }
    public int citizenId { get; set; }
    public string address { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
}
