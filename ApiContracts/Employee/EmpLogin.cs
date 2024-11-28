using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmpLogin
{
    [JsonPropertyName("userName")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
