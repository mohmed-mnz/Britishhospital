using ApiContracts;
using ApiContracts.Organization;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace BussinesLayer.Services;

public class organizationServieces(IOrganizationRepository _repository, IMapper _mapper) : IorganizationServieces
{
    public async Task<GResponse<OrganizationDto>> AddORganization(OrganizationAddDto organizationDto)
    {
        var org = _mapper.Map<Organization>(organizationDto);
        await _repository.InsertAsync(org);
        await _repository.Commit();
        var orgDto = _mapper.Map<OrganizationDto>(org);
        return GResponse<OrganizationDto>.CreateSuccess(orgDto);
    }

    public async Task<GResponse<bool>> DeleteOrganization(int orgid)
    {
        var org = await _repository.Where(x => x.Orgid == orgid)!.FirstOrDefaultAsync();
        if (org == null)
            throw new ApplicationException("organizationNotFound");
        _repository.Delete(org);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<OrganizationDto>> GetOrganizationbyorgid(int orgid)
    {
        var org = await _repository.Where(x => x.Orgid == orgid)!.FirstOrDefaultAsync();
        if (org == null)
            throw new ApplicationException("organizationNotFound");
        var orgDto = _mapper.Map<OrganizationDto>(org);
        return GResponse<OrganizationDto>.CreateSuccess(orgDto);
    }

    public async Task<GResponse<IEnumerable<OrganizationDto>>> GetOrganizationsAsync()
    {
        var orgs = await _repository.GetAllAsync();
        var orgDtos = _mapper.Map<IEnumerable<OrganizationDto>>(orgs);
        return GResponse<IEnumerable<OrganizationDto>>.CreateSuccess(orgDtos);

    }

    public async Task<GResponse<OrganizationDto>> UpdateOrganization(OrganizationUpdateDto organizationDto)
    {
        var org = await _repository.Where(x => x.Orgid == organizationDto.Orgid)!.FirstOrDefaultAsync();
        if (org == null)
            throw new ApplicationException("organizationNotFound");
        org = _mapper.Map(organizationDto, org);
        await _repository.Commit();
        var orgDto = _mapper.Map<OrganizationDto>(org);
        return GResponse<OrganizationDto>.CreateSuccess(orgDto);
    }
}
