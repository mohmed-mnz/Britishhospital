using ApiContracts.groupusers;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupUserController(IGroupUserServices services) : ControllerBase
{
    [HttpGet("get-all-Group-users")]
    public async Task<IActionResult> GetAllGroupUsers()
    {
        var result = await services.GetGroupUsersAsync();
        return Ok(result);
    }
    [HttpGet("get-Group-user-by-id/{id}")]
    public async Task<IActionResult> GetGroupUserById(int id)
    {
        var result = await services.GetGroupUserAsync(id);
        return Ok(result);
    }
    [HttpPost("add-Group-user")]
    public async Task<IActionResult> AddGroupUser(GroupUserAddDto groupUserAddDto)
    {
        var result = await services.AddGroupUserAsync(groupUserAddDto);
        return Ok(result);
    }
    [HttpPut("update-Group-user")]
    public async Task<IActionResult> UpdateGroupUser(GroupUserUpdateDto groupUserUpdateDto)
    {
        var result = await services.UpdateGroupUserAsync(groupUserUpdateDto);
        return Ok(result);
    }
    [HttpDelete("delete-Group-user/{id}")]
    public async Task<IActionResult> DeleteGroupUser(int id)
    {
        var result = await services.DeleteGroupUserAsync(id);
        return Ok(result);
    }
    [HttpGet("get-all-group-user-based-on-Group-id/{GrouId}")]
    public async Task<IActionResult> GetAllBasedOnGroupId(int GrouId)
    {
        var result =await services.GetGroupUsersByGroupIdAsync(GrouId);
        return Ok(result);
    }
    [HttpGet("get-all-group-user-based-on-Emp-id/{EmpId}")]
    public async Task<IActionResult> GetAllBasedOnEmpID(int EmpId)
    {
        var result = await services.GetGroupUsersByUserIdAsync(EmpId);
        return Ok(result);
    }
}
