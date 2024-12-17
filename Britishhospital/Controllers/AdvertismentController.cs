using ApiContracts.Advertisment;
using BussinesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AdvertismentController(IAdvertismentServices _services) : ControllerBase
{
    [HttpGet("get-all-Advertisments")]
    public async Task<IActionResult> GetAllAdvertisments()
    {
        var result = await _services.GetAllAdvertisments();
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-Advertisment-by-id/{id}")]
    public async Task<IActionResult> GetAdvertismentById(int id)
    {
        var result = await _services.GetAdvertismentById(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPost("add-Advertisment")]
    public async Task<IActionResult> AddAdvertisment(AdvertismentAddDto model)
    {
        var result = await _services.AddAdvertisment(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("update-Advertisment")]
    public async Task<IActionResult> UpdateAdvertisment(AdvertismentUpdateDto model)
    {
        var result = await _services.UpdateAdvertisment(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("delete-Advertisment/{id}")]
    public async Task<IActionResult> DeleteAdvertisment(int id)
    {
        var result = await _services.DeleteAdvertisment(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
}
