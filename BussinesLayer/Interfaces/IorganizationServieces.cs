using ApiContracts;
using ApiContracts.Organization;

namespace BussinesLayer.Interfaces;

public interface IorganizationServieces
{
    Task<GResponse<IEnumerable<OrganizationDto>>> GetOrganizationsAsync();
    Task<GResponse<OrganizationDto>> GetOrganizationbyorgid(int orgid);
    Task<GResponse<OrganizationDto>>AddORganization(OrganizationAddDto organizationDto);
    Task<GResponse<OrganizationDto>>UpdateOrganization(OrganizationUpdateDto organizationDto);

    Task<GResponse<bool>>DeleteOrganization(int orgid);
}
