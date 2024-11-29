using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmployeeLoginDto
{

    [JsonPropertyName("empId")]
    public int EMPID { get; set; }
    [JsonPropertyName("orgId")]
    public int ORGID { get; set; }
    [JsonPropertyName("citizenId")]
    public int CTZNID { get; set; }
    [JsonPropertyName("Token")]
    public string Token { get; set; } = string.Empty;
    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("nid")]
    public string NID { get; set; } = string.Empty;
    [JsonPropertyName("counterId")]
    public int? CounterId { get; set; }


    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = string.Empty;

    [JsonPropertyName("expiryTimeinMinutes")]
    public int ExpirytimeinMinutes { get; set; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; } = new List<string>();


}
