using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class AdminReservationStatisticsBasedOneveryDayInTheWeekDto
{
    [JsonPropertyName("totalCount")]
    public int totalcount { get; set; }

    [JsonPropertyName("totalServedCount")]
    public int totalservedcount { get; set; }
    [JsonPropertyName("day")]
    public string day { get; set; } = string.Empty;
    [JsonPropertyName("averageCount")]
    public double averagecount { get; set; }
    [JsonPropertyName("averageServedcount")]
    public double averageservedcount { get; set; }
}
