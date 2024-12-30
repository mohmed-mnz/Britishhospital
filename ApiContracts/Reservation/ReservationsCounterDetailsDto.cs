﻿using System.Text.Json.Serialization;

namespace ApiContracts.Reservation;

public class ReservationsCounterDetailsDto
{
    [JsonPropertyName("counterName")]
    public string? CounterName { get; set; }
    [JsonPropertyName("reservationData")]
    public List<ReservationsDto> ReservationData { get; set; } = new List<ReservationsDto>();
}