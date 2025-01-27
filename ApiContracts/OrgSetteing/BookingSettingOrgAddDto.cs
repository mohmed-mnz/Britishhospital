using System.Globalization;
using System.Text.Json.Serialization;

namespace ApiContracts.OrgSetteing;

public class BookingSettingOrgAddDto
{
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
    public void NormalizeTimeFormats()
    {
        StartWorkingHour = NormalizeTime(StartWorkingHour);
        EndWorkingHour = NormalizeTime(EndWorkingHour);
        KioskClosingTime = NormalizeTime(KioskClosingTime);
    }

    private string? NormalizeTime(string? time)
    {
        if (string.IsNullOrWhiteSpace(time)) return null;

        if (DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed24Hour))
        {
            return parsed24Hour.ToString("hh:mmtt", CultureInfo.InvariantCulture);
        }

        if (DateTime.TryParseExact(time, "hh:mmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed12Hour))
        {
            return parsed12Hour.ToString("hh:mmtt", CultureInfo.InvariantCulture);
        }

        throw new FormatException($"Invalid time format: {time}");
    }
}
