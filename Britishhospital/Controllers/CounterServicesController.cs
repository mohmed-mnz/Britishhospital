using ApiContracts.CounterService;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CounterServicesController : ControllerBase
{
    private readonly ICounterServicesServices _counterServicesServices;

    public CounterServicesController(ICounterServicesServices counterServicesServices)
    {
        _counterServicesServices = counterServicesServices;
    }
    [HttpGet]
    [Route("get-all-counter-services")]
    public async Task<IActionResult> GetAllCounterServices()
    {
        var result = await _counterServicesServices.GetAsync();
        return Ok(result);
    }
    [HttpGet]
    [Route("get-counter-services-by-counter-id/{counterId}")]
    public async Task<IActionResult> GetCounterServicesByCounterId(int counterId)
    {
        var result = await _counterServicesServices.GetbasedonCounterIdAsync(counterId);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-counter-services-by-id/{id}")]
    public async Task<IActionResult> GetCounterServicesById(int id)
    {
        var result = await _counterServicesServices.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [Route("create-counter-services")]
    public async Task<IActionResult> CreateCounterServices(CounterServicesDto counterServicesDto)
    {
        var result = await _counterServicesServices.CreateAsync(counterServicesDto);
        return Ok(result);
    }

    [HttpPut]
    [Route("update-counter-services")]
    public async Task<IActionResult> UpdateCounterServices(CounterServicesUpdateDto counterServicesDto)
    {
        var result = await _counterServicesServices.UpdateAsync(counterServicesDto);
        return Ok(result);
    }
    [HttpDelete]
    [Route("delete-counter-services/{id}")]
    public async Task<IActionResult> DeleteCounterServices(int id)
    {
        var result = await _counterServicesServices.DeleteAsync(id);
        return Ok(result);
    }

}
