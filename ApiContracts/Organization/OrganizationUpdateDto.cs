using System.Text.Json.Serialization;

namespace ApiContracts.Organization;

public class OrganizationUpdateDto
{
    [JsonPropertyName("orgId")]
    public int Orgid { get; set; }
    [JsonPropertyName("orgName")]
    public string OrgName { get; set; } = null!;
}
