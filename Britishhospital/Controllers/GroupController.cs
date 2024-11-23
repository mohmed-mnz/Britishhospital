using ApiContracts.Group;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController(IGroupServices _services ) : ControllerBase
{
    [HttpGet("get-all-groups") ]
    public async Task<IActionResult> GetGroups()
    {
        var result = await _services.GetGroupsAsync();
        return Ok(result);
    }
    [HttpGet("get-group-by-id/{id}")]
    public async Task<IActionResult> GetGroupById(int id)
    {
        var result = await _services.GetGroupAsync(id);
        return Ok(result);
    }
    [HttpPost("add-group")]
    public async Task<IActionResult> AddGroup(GroupAddDto groupAddDto)
    {
        var result = await _services.AddGroupAsync(groupAddDto);
        return Ok(result);
    }
    [HttpPut("update-group")]
    public async Task<IActionResult> UpdateGroup(GroupUpdateDto groupUpdateDto)
    {
        var result = await _services.UpdateGroupAsync(groupUpdateDto);
        return Ok(result);
    }
    [HttpDelete("delete-group/{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        var result = await _services.DeleteGroupAsync(id);
        return Ok(result);
    }

}
