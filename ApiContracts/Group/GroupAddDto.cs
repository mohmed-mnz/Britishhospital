using System.Text.Json.Serialization;

namespace ApiContracts.Group;

public class GroupAddDto
{
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

}
