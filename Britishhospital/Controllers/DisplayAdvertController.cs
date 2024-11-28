using ApiContracts.DisplayAdvert;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DisplayAdvertController : ControllerBase
{
    private readonly IDisplayAdvertsServices _displayAdvertsServices;

    public DisplayAdvertController(IDisplayAdvertsServices displayAdvertsServices)
    {
        _displayAdvertsServices = displayAdvertsServices;
    }
    [HttpGet("get-all-display-adverts")]
    public async Task<IActionResult> GetAllDisplayAdverts()
    {
        var response = await _displayAdvertsServices.GetAdvertsAsync();
        return Ok(response);
    }
    [HttpGet("get-display-advert/{id}")]
    public async Task<IActionResult> GetDisplayAdvert(int id)
    {
        var response = await _displayAdvertsServices.GetAdvertAsync(id);
        return Ok(response);
    }
    [HttpPost("add-display-advert")]
    public async Task<IActionResult> AddDisplayAdvert([FromBody] DisplayAdvertsAddDto advert)
    {
        var response = await _displayAdvertsServices.AddAdvertAsync(advert);
        return Ok(response);
    }
    [HttpDelete("delete-display-advert/{id}")]
    public async Task<IActionResult> DeleteDisplayAdvert(int id)
    {
        var response = await _displayAdvertsServices.DeleteAdvertAsync(id);
        return Ok(response);
    }
    [HttpPut("update-display-advert")]
    public async Task<IActionResult> UpdateDisplayAdvert([FromBody] DisplayAdvertsUpdateDto advert)
    {
        var response = await _displayAdvertsServices.UpdateAdvertAsync(advert);
        return Ok(response);
    }
    [HttpGet("get-display-adverts-by-display/{displayId}")]
    public async Task<IActionResult> GetDisplayAdvertsByDisplay(int displayId)
    {
        var response = await _displayAdvertsServices.GetAdvertsByDisplayAsync(displayId);
        return Ok(response);
    }

}
