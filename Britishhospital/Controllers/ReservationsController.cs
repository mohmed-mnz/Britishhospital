using ApiContracts.Reservation;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class ReservationsController : ControllerBase
{
    private readonly IReservationServices _Services;

    public ReservationsController(IReservationServices services)
    {
        _Services = services;
    }
    [HttpPost]
    [Route("get-reservations-based-on-orgid-and-counterid")]
    public async Task<IActionResult> GetReservationsByOrgId(ReservationFilterDto dto)
    {
        var result = await _Services.GetReservationsByOrgId(dto.OrgId,dto.CounterId);
        return Ok(result);
    }
    [HttpPost]
    [Route("add-reservation")]
    public async Task<IActionResult> AddReservation([FromBody] ReservationsAddDto reservation)
    {
        var result = await _Services.AddReservation(reservation);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-reservation/{id}")]
    public async Task<IActionResult> GetReservation([FromQuery] long id)
    {
        var result = await _Services.GetReservation(id);
        return Ok(result);
    }
    [HttpPut]
    [Route("update-reservation-status")]
    public async Task<IActionResult> UpdateReservation([FromBody] ReservationsUpdateStatusDto reservation)
    {
        var result = await _Services.UpdateReservation(reservation);
        return Ok(result);
    }
    [HttpDelete]
    [Route("cancell-reservation")]
    public async Task<IActionResult> CancellReservation(long id)
    {
        var result = await _Services.CancellReservation(id);
        return Ok(result);
    }
    [HttpPost]
    [Route("call-next-in-queue")]
    public async Task<IActionResult> CallNextInQueue([FromBody]CallNextInqueueReq callnextinqueuereq)
    {
        var result = await _Services.CallNextInQueue(callnextinqueuereq);
        return Ok(result);
    }
    [HttpPost]
    [Route("reservation-statistics-based-on-orgid")]
    public async Task<IActionResult> ReservationStatisticsBasedOnOrgId([FromBody] FilterReservationStatistics filter)
    {
        var result = await _Services.reservationstatisticsbasedonorgid(filter);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-reservations-based-on-orgid/{orgId}")]
    public async Task<IActionResult> GetReservationsBasedOnOrgId(int orgId)
    {
        var result = await _Services.GetReservationsBasedOnOrgId(orgId);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-serving-reservations-based-on-orgid/{orgId}")]
    public async Task<IActionResult> GetServingReservationsBasedOnOrgId(int orgId)
    {
        var result = await _Services.GetServingReservationsBasedOnOrgId(orgId);
        return Ok(result);
    }
    [HttpPost]
    [Route("get-reservation-statistics-based-on-every-day-in-the-week")]
    public async Task<IActionResult> GetReservationStatisticsBasedOnEveryDayInTheWeek([FromBody] FilterReservationStatistics filterData)
    {
        var result = await _Services.GetReservationStatisticsBasedOnEveryDayInTheWeek(filterData);
        return Ok(result);
    }
    [HttpPost]
    [Route("get-reservations-serving-by-orgid-and-counterid")]
    public async Task<IActionResult> GetReservationsServingByOrgId(ReservationFilterDto dto)
    {
        var result = await _Services.GetReservationsServingByOrgId(dto.OrgId, dto.CounterId);
        return Ok(result);
    }


}
