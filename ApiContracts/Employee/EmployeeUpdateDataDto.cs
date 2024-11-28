using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmployeeUpdateDataDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("citizenId")]
    public int citizenId { get; set; }
    [JsonPropertyName("address")]
    public string address { get; set; } = string.Empty;
    [JsonPropertyName("email")]
    public string email { get; set; } = string.Empty;
    [JsonPropertyName("mobile")]
    public string Mobile { get; set; } = string.Empty;
}
