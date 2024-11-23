using AutoMapper;

namespace BussinesLayer.Mapping.UserGroups;

public class GroupUserProfile : Profile
{
    public GroupUserProfile()
    {
        CreateMap<Models.Models.GroupUser, ApiContracts.groupusers.GroupUserDto>()
            .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.GroupName))
            .ForMember(dest => dest.EmpName, opt => opt.MapFrom(src => src.Emp.Citizen!.Name))
            .ReverseMap();
        CreateMap<ApiContracts.groupusers.GroupUserAddDto, Models.Models.GroupUser>().ReverseMap();
        CreateMap<ApiContracts.groupusers.GroupUserUpdateDto, Models.Models.GroupUser>().ReverseMap();
    }
}
