using ApiContracts.Organization;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Organization;

public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<OrganizationAddDto, Models.Models. Organization>();
        CreateMap<OrganizationUpdateDto, Models.Models.Organization>();
        CreateMap<Models.Models.Organization, OrganizationDto>();
    }
}
