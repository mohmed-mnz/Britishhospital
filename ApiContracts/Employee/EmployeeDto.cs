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
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new List<string>();

    [JsonPropertyName("citizenId")]
    public int CitizenId { get; set; }
    [JsonPropertyName("nid")]
    public string? Nid { get;set; }

    [JsonPropertyName("isActicve")]
    public bool? isActicve { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }

}
