using System.Text.Json.Serialization;

namespace ApiContracts.groupusers;

public class GroupUserAddDto
{
    [JsonPropertyName("groupId")]
    public int? GroupId { get; set; }
    [JsonPropertyName("empId")]
    public int? EmpId { get; set; }

}
