using System.Text.Json.Serialization;

namespace ApiContracts.Group;

public class GroupDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("groupName")]
    public string? GroupName { get; set; }

}
