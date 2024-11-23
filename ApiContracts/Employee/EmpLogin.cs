using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmpLogin
{
    [JsonPropertyName("UserName")]
    public string Username { get; set; } = string.Empty;
    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;
}
