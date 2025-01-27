using System.Text.Json.Serialization;

namespace ApiContracts.OrgSetteing;

public class BookingSettingOrgUpdateDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("startWorkinghour")]
    public string? StartWorkingHour { get; set; }
    [JsonPropertyName("endWorkinghour")]
    public string? EndWorkingHour { get; set; }
    [JsonPropertyName("orgId")]
    public int? OrgId { get; set; }
    [JsonPropertyName("kioskClosingtime")]
    public string? KioskClosingTime { get; set; }
    [JsonPropertyName("userLimitreservation")]
    public int? UserlimitReservation { get; set; }

}
