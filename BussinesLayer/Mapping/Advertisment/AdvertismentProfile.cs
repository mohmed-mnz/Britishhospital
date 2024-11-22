using ApiContracts.Advertisment;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Advertisment;

public class AdvertismentProfile : Profile
{
    public AdvertismentProfile()
    {
        CreateMap<Models.Models.Advertisment, AdvertismentDto>()
            .ReverseMap();
        CreateMap<AdvertismentAddDto, Models.Models. Advertisment>()
            .ReverseMap();
        CreateMap<AdvertismentUpdateDto, Models.Models. Advertisment>()
            .ReverseMap();
    }
}
