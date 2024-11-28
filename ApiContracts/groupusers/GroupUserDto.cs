using System.Text.Json.Serialization;

namespace ApiContracts.groupusers;

public class GroupUserDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("groupId")]
    public int? GroupId { get; set; }
    [JsonPropertyName("empId")]
    public int? EmpId { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
    [JsonPropertyName("empName")]
    public string? EmpName { get; set; }
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

}
