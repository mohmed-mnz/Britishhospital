namespace ApiContracts.Employee;

public class EmpChangePasswordDto
{
  public string OldPass { get; set; } = string.Empty;

    public string NewPass { get; set; } = string.Empty;
    public  int empId { get; set; }
}
