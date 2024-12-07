using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class CallNextInqueueReq
{
    [JsonPropertyName("orgId")]
    public int OrgId { get; set; }
    [JsonPropertyName("counterId")]
    public int CounterId { get; set; }
    [JsonPropertyName("serviceId")]
    public int ServiceId { get; set; }
}
