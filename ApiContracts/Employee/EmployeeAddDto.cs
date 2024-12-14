using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmployeeAddDto
{
    [JsonPropertyName("userName")]
    public string Username { get; set; } = null!;
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
    [JsonPropertyName("orgId")]
    public int? Orgid { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [MaxLength(11)]
    [JsonPropertyName("mobileNumber")]
    public string? MobileNumber { get; set; }
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [Required]
    [MaxLength(14)]
    [JsonPropertyName("nid")]
    public string? Nid { get; set; }
    [JsonPropertyName("gender")]
    public bool? gender { get; set; }
}
