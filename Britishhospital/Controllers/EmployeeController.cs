using ApiContracts.Employee;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IEmployeeServices _services) : ControllerBase
{
    [HttpGet("get-all-employees")]
    public async Task<IActionResult> GetAllEmployees()
    {
        var result = await _services.GetEmployeeListAsync();
        return Ok(result);
    }
    [HttpGet("get-employee-by-id/{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var result = await _services.GetEmployeeByIdAsync(id);
        return Ok(result);
    }
    [HttpGet("get-employee-by-org-id/{orgId}")]
    public async Task<IActionResult> GetEmployeeByOrgId(int orgId)
    {
        var result = await _services.GetEmployeeByOrgIdListAsync(orgId);
        return Ok(result);
    }
    [HttpDelete("delete-employee/{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _services.DeleteEmployeeAsync(id);
        return Ok(result);
    }
    [HttpPost("register-employee")]
    public async Task<IActionResult> RegisterEmployee(EmployeeAddDto employeeAddDto)
    {
        var result = await _services.RegestierEmployee(employeeAddDto);
        return Ok(result);
    }
    [HttpPut("update-employee-data")]
    public async Task<IActionResult> UpdateEmployeeData(EmployeeUpdateDataDto employeeUpdateDataDto)
    {
        var result = await _services.UpdateEmployeeData(employeeUpdateDataDto);
        return Ok(result);
    }
    [HttpPut("set-employee-as-not-active/{empid}")]
    public async Task<IActionResult> SetEmployeeAsNotActive(int empid)
    {
        var result = await _services.SetEmployeeAsNotActive(empid);
        return Ok(result);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(EmpLogin loginDto)
    {
        var result = await _services.Login(loginDto);
        return Ok(result);
    }

}
