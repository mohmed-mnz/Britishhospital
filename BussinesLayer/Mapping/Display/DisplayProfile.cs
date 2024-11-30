using ApiContracts.Display;
using AutoMapper;
namespace BussinesLayer.Mapping.Display;

public class DisplayProfile : Profile
{
    public DisplayProfile()
    {
        CreateMap<DisplayAddDto, Models.Models.Display>()
            .ReverseMap();
        CreateMap<DisplayUpdateDto, Models.Models.Display>()
            .ReverseMap();
        CreateMap<Models.Models.Display, DisplayDto>()
            .ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org!.OrgName))
            .ReverseMap();
    }
}
