using Models.Models;

namespace ApiContracts.Employee;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;

    public string OrgName { get; set; } = null!;
    public string Email { get; set; }
    public string Phone { get; set; } 
    public string CitizenName { get; set; }

    public List<GroupUser> groupUsers { get; set; }


}
