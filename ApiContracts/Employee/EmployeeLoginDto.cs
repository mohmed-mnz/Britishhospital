using System.Text.Json.Serialization;

namespace ApiContracts.Employee;

public class EmployeeLoginDto
{

    [JsonPropertyName("EmpId")]
    public int EMPID { get; set; }
    [JsonPropertyName("OrgId")]
    public int ORGID { get; set; }
    [JsonPropertyName("CtznId")]
    public int CTZNID { get; set; }
    [JsonPropertyName("Token")]
    public string Token { get; set; } = string.Empty;
    [JsonPropertyName("UserName")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("Nid")]
    public string NID { get; set; } = string.Empty;
    [JsonPropertyName("CounterId")]
    public int? CounterId { get; set; }


    [JsonPropertyName("OrgName")]
    public string OrgName { get; set; } = string.Empty;

    [JsonPropertyName("ExpirytimeinMinutes")]
    public int ExpirytimeinMinutes { get; set; }
}
