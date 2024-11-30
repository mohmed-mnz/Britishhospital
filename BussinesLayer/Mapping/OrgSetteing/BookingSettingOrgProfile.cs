using AutoMapper;

namespace BussinesLayer.Mapping.OrgSetteing;

public class BookingSettingOrgProfile : Profile
{
    public BookingSettingOrgProfile()
    {
        CreateMap<Models.Models.BookingSettingOrg, ApiContracts.OrgSetteing.BookingSettingOrgDto>()
            .ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org!.OrgName))
            .ReverseMap();
        CreateMap<ApiContracts.OrgSetteing.BookingSettingOrgAddDto, Models.Models.BookingSettingOrg>()
            .ReverseMap();
        CreateMap<ApiContracts.OrgSetteing.BookingSettingOrgUpdateDto, Models.Models.BookingSettingOrg>()
            .ReverseMap();
    }
}
