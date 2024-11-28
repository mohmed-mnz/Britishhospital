using ApiContracts.DisplayCounter;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DisplayCountersController : ControllerBase
{
    private readonly IDisplayCounterServices _displayCountersServices;

    public DisplayCountersController(IDisplayCounterServices displayCountersServices)
    {
        _displayCountersServices = displayCountersServices;
    }
    [HttpGet("get-all-display-counters")]
    public async Task<IActionResult> GetAllDisplayCounters()
    {
        var response = await _displayCountersServices.GetCountersAsync();
        return Ok(response);
    }
    [HttpGet("get-display-counter/{id}")]
    public async Task<IActionResult> GetDisplayCounter(int id)
    {
        var response = await _displayCountersServices.GetCounterAsync(id);
        return Ok(response);
    }
    [HttpPost("add-display-counter")]
    public async Task<IActionResult> AddDisplayCounter([FromBody] DisplayCountersAddDto counter)
    {
        var response = await _displayCountersServices.AddCounterAsync(counter);
        return Ok(response);
    }
    [HttpDelete("delete-display-counter/{id}")]
    public async Task<IActionResult> DeleteDisplayCounter(int id)
    {
        var response = await _displayCountersServices.DeleteCounterAsync(id);
        return Ok(response);
    }
    [HttpPut("update-display-counter")]
    public async Task<IActionResult> UpdateDisplayCounter([FromBody] DisplayCountersUpdateDto counter)
    {
        var response = await _displayCountersServices.UpdateCounterAsync(counter);
        return Ok(response);
    }
    [HttpGet("get-display-counters-by-display/{displayId}")]
    public async Task<IActionResult> GetDisplayCountersByDisplay(int displayId)
    {
        var response = await _displayCountersServices.GetCountersByDisplayAsync(displayId);
        return Ok(response);
    }
}
