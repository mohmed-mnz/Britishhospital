using ApiContracts.Organization;
using BussinesLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritshHospital.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]

public class OrganizationController : ControllerBase
{
    private readonly IorganizationServieces _organizationServieces;

    public OrganizationController(IorganizationServieces organizationServieces)
    {
        _organizationServieces = organizationServieces;
    }
    [HttpGet]
    [Route("get-all-organizations")]
    public async Task<IActionResult> GetOrganizations()
    {
        var result = await _organizationServieces.GetOrganizationsAsync();
        return Ok(result);
    }
    [HttpPost]
    [Route("add-organization")]
    public async Task<IActionResult> AddOrganization(OrganizationAddDto organizationDto)
     {
        var result = await _organizationServieces.AddORganization(organizationDto);
        return Ok(result);
    }
    [HttpPut]
    [Route("update-organization")]
    public async Task<IActionResult> UpdateOrganization(OrganizationUpdateDto organizationDto)
    {
        var result = await _organizationServieces.UpdateOrganization(organizationDto);
        return Ok(result);
    }
    [HttpDelete]
    [Route("delete-organization/{orgid}")]
    public async Task<IActionResult> DeleteOrganization(int orgid)
    {
        var result = await _organizationServieces.DeleteOrganization(orgid);
        return Ok(result);
    }
    [HttpGet]
    [Route("get-organization-by-id/{orgid}")]
    public async Task<IActionResult> GetOrganizationById(int orgid)
    {
        var result = await _organizationServieces.GetOrganizationbyorgid(orgid);
        return Ok(result);
    }

}
