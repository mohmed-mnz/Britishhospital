using ApiContracts.Counters;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CounterController(ICountersServices _services) : ControllerBase
{
    [HttpGet("get-all-Counters")]
    public async Task<IActionResult> GetAllCounters()
    {
        var result = await _services.GetAllCounters();
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-Counter-by-id/{id}")]
    public async Task<IActionResult> GetCounterById(int id)
    {
        var result = await _services.GetCounterbyId(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPost("add-Counter")]
    public async Task<IActionResult> AddCounter(CountersAddDto model)
    {
        var result = await _services.AddCounter(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpPut("update-Counter")]
    public async Task<IActionResult> UpdateCounter(CountersUpdateDto model)
    {
        var result = await _services.UpdateCounter(model);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("delete-Counter/{id}")]
    public async Task<IActionResult> DeleteCounter(int id)
    {
        var result = await _services.DeleteCounter(id);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }
    [HttpGet("get-all-Counters-based-on-orgid/{orgid}")]
    public async Task<IActionResult> GetAllCountersBasedOnOrgId(int orgid)
    {
        var result = await _services.GetAllBasedonorgid(orgid);
        return result.IsSucceeded ? Ok(result) : BadRequest(result);
    }


}
