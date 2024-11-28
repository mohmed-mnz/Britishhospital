using AutoMapper;

namespace BussinesLayer.Mapping.DisplayAdverts;

public class DisplayAdvertsProfile : Profile
{
    public DisplayAdvertsProfile()
    {
        CreateMap<Models.Models.DisplayAdverts, ApiContracts.DisplayAdvert.DisplayAdvertsDto>()
            .ForMember(destinationMember => destinationMember.AdvertName, memberOptions => memberOptions.MapFrom(source => source.Advert.AdvertName))
            .ForMember(destinationMember => destinationMember.DisplayName, memberOptions => memberOptions.MapFrom(source => source.Display.DisplayName))
            .ReverseMap();
        CreateMap<ApiContracts.DisplayAdvert.DisplayAdvertsAddDto, Models.Models.DisplayAdverts>()
            .ReverseMap();
        CreateMap<ApiContracts.DisplayAdvert.DisplayAdvertsUpdateDto, Models.Models.DisplayAdverts>()
            .ReverseMap();
    }
}
