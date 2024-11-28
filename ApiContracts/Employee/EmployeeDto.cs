using Models.Models;
using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmployeeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("userName")]
    public string Username { get; set; } = null!;
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = null!;
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
    [JsonPropertyName("phone")]
    public string Phone { get; set; } = null!;
    [JsonPropertyName("citizenName")]
    public string CitizenName { get; set; } = null!;
    public List<GroupUser> groupUsers { get; set; }


}
