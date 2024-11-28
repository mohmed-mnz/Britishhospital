using System.ComponentModel.DataAnnotations;

namespace ApiContracts.Employee;

public class EmployeeAddDto
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? Orgid { get; set; }
     
    public string? Email { get; set; }
    [MaxLength(11)]
    public string? MobileNumber { get; set; }
    public string? Address { get; set; }
    public string? Name { get; set; }
    [Required]
    [MaxLength(14)]
    public string? Nid { get; set; }
    public bool? gender { get; set; }
}
