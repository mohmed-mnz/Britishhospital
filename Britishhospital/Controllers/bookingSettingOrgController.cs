using ApiContracts.OrgSetteing;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class bookingSettingOrgController : ControllerBase
{
    private readonly IBookingSettingOrgServices _bookingSettingOrgServices;

    public bookingSettingOrgController(IBookingSettingOrgServices bookingSettingOrgServices)
    {
        _bookingSettingOrgServices = bookingSettingOrgServices;
    }
    [HttpGet]
    [Route("get-all-org-setteing-based-on-org-id/{orgid}")]
    public async Task<IActionResult> GetBookingSettingOrgsAsync(int orgid)
    {
        var result = await _bookingSettingOrgServices.GetBookingSettingOrgsAsync(orgid);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-org-setteing-based-on-id/{id}")]
    public async Task<IActionResult> GetBookingSettingOrgAsync(int id)
    {
        var result = await _bookingSettingOrgServices.GetBookingSettingOrgAsync(id);
        return Ok(result);
    }
    [HttpPost]
    [Route("add-org-setteing")]
    public async Task<IActionResult> AddBookingSettingOrgAsync(BookingSettingOrgAddDto bookingSettingOrgAddDto)
    {
        var result = await _bookingSettingOrgServices.AddBookingSettingOrgAsync(bookingSettingOrgAddDto);
        return Ok(result);
    }
    [HttpPut]
    [Route("update-org-setteing")]
    public async Task<IActionResult> UpdateBookingSettingOrgAsync(BookingSettingOrgUpdateDto bookingSettingOrgUpdateDto)
    {
        var result = await _bookingSettingOrgServices.UpdateBookingSettingOrgAsync(bookingSettingOrgUpdateDto);
        return Ok(result);
    }
    [HttpDelete]
    [Route("delete-org-setteing/{id}")]
    public async Task<IActionResult> DeleteBookingSettingOrgAsync(int id)
    {
        var result = await _bookingSettingOrgServices.DeleteBookingSettingOrgAsync(id);
        return Ok(result);
    }

}
