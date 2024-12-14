using ApiContracts.Employee;
using AutoMapper;

namespace BussinesLayer.Mapping.Employee;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Models.Models.Employee, EmployeeDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.isActicve, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org!.OrgName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Citizen!.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Citizen!.Mobile))
            .ForMember(dest => dest.CitizenName, opt => opt.MapFrom(src => src.Citizen!.Name))
            .ForMember(dest => dest.CitizenId, opt => opt.MapFrom(src => src.Citizen!.CitizenId))
            .ForMember(dest => dest.Nid, opt => opt.MapFrom(src => src.Citizen!.Nid))
            .ReverseMap();
        CreateMap<EmployeeAddDto, Models.Models.Employee>().ReverseMap();
        CreateMap<EmployeeUpdateDataDto, Models.Models.Employee>().ReverseMap();
    }
}
