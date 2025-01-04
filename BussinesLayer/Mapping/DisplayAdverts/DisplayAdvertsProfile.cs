using ApiContracts.DisplayAdvert;
using AutoMapper;

namespace BussinesLayer.Mapping.DisplayAdverts;

public class DisplayAdvertsProfile : Profile
{
    public DisplayAdvertsProfile()
    {
        CreateMap<Models.Models.DisplayAdverts, DisplayAdvertsDto>()
            .ForMember(destinationMember => destinationMember.AdvertName, memberOptions => memberOptions.MapFrom(source => source.Advert.AdvertName))
            .ForMember(destinationMember => destinationMember.DisplayName, memberOptions => memberOptions.MapFrom(source => source.Display.DisplayName))
            .ReverseMap();
        CreateMap<DisplayAdvertsAddDto, Models.Models.DisplayAdverts>()
            .ReverseMap();
        CreateMap<DisplayAdvertsUpdateDto, Models.Models.DisplayAdverts>()
            .ReverseMap();
        CreateMap<Models.Models.DisplayAdverts, DisplayAdvertsTinyDto>()
            .ForMember(dest => dest.AdvertId, opt => opt.MapFrom(src => src.AdvertId))
            .ForMember(dest => dest.AdvertName, opt => opt.MapFrom(src => src.Advert.AdvertName))
            .ForMember(dest => dest.Mediatype, opt => opt.MapFrom(src => src.Advert.Mediatype))
            .ForMember(dest => dest.MediaFile, opt => opt.MapFrom(src => src.Advert.MediaFile));


    }
}
