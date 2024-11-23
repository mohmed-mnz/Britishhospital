using AutoMapper;

namespace BussinesLayer.Mapping.Group;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Models.Models.Group, ApiContracts.Group.GroupDto>().ReverseMap();
        CreateMap<ApiContracts.Group.GroupAddDto, Models.Models.Group>().ReverseMap();
        CreateMap<ApiContracts.Group.GroupUpdateDto, Models.Models.Group>().ReverseMap();
    }
}
