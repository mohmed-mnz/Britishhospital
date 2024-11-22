using ApiContracts.Service;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController(IServicesServices _Services) : ControllerBase
{
    [HttpGet("get-all-services")]
    public async Task<IActionResult> GetServicesAsync()
    {
        var result = await _Services.GetServicesAsync();
        return Ok(result);
    }
    [HttpGet("get-service-by-id/{id}")]
    public async Task<IActionResult> GetServiceByIdAsync(int id)
    {
        var result = await _Services.GetServiceByIdAsync(id);
        return Ok(result);
    }
    [HttpPost("add-service")]
    public async Task<IActionResult> AddServiceAsync(ServiceAddDto serviceAddDto)
    {
        var result = await _Services.AddServiceAsync(serviceAddDto);
        return Ok(result);
    }
    [HttpPut("update-service")]
    public async Task<IActionResult> UpdateServiceAsync(ServiceUpdateDto serviceUpdateDto)
    {
        var result = await _Services.UpdateServiceAsync(serviceUpdateDto);
        return Ok(result);
    }
    [HttpDelete("delete-service/{id}")]
    public async Task<IActionResult> DeleteServiceAsync(int id)
    {
        var result = await _Services.DeleteServiceAsync(id);
        return Ok(result);
    }


}
