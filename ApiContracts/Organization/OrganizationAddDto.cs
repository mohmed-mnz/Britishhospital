using System.Text.Json.Serialization;

namespace ApiContracts.Organization;

public class OrganizationAddDto
{
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = null!;
}
