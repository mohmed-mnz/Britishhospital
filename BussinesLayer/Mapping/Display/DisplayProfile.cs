using ApiContracts.Display;
using ApiContracts.DisplayAdvert;
using ApiContracts.DisplayCounter;
using AutoMapper;
using Models.Models;
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

        CreateMap<Models.Models.Display, DisplayDetailsDto>()
            .ForMember(dest => dest.OrgName, opt => opt.MapFrom(src => src.Org!.OrgName))
            .ForMember(dest => dest.DisplayAdverts, opt => opt.MapFrom(src => src.DisplayAdverts))
            .ForMember(dest => dest.DisplayCounters, opt => opt.MapFrom(src => src.DisplayCounters))
            .ReverseMap();  
    }
}
