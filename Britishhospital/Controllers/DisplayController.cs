using ApiContracts.Display;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DisplayController(IDisplayServices _services) : ControllerBase
{

    [HttpGet("get-all-displays")]
    public async Task<IActionResult> GetAllDisplays()
    {
        var result = await _services.GetAllDisplays();
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-display-by-id/{id}")]
    public async Task<IActionResult> GetDisplayById(int id)
    {
        var result = await _services.GetDisplaybyId(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPost("add-display")]
    public async Task<IActionResult> AddDisplay(DisplayAddDto model)
    {
        var result = await _services.AddDisplay(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("update-display")]
    public async Task<IActionResult> UpdateDisplay(DisplayUpdateDto model)
    {
        var result = await _services.UpdateDisplay(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("delete-display/{id}")]
    public async Task<IActionResult> DeleteDisplay(int id)
    {
        var result = await _services.DeleteDisplay(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-all-displays-based-on-orgid/{orgid}")]
    public async Task<IActionResult> GetAllDisplaysBasedOnOrgId(int orgid)
    {
        var result = await _services.GetAllBasedonorgid(orgid);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-display-details/{displayId}")]
    public async Task<IActionResult> GetDisplayDetails(int displayId)
    {
        var result = await _services.GetDisplayDetails(displayId);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
}
