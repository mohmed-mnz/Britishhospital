using System.Text.Json.Serialization;

namespace ApiContracts.groupusers;

public class GroupUserUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("groupId")]
    public int? GroupId { get; set; }
    [JsonPropertyName("empId")]
    public int? EmpId { get; set; }
   
}
