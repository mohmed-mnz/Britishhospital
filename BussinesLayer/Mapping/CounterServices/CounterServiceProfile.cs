using ApiContracts.CounterService;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.CounterServices;

public class CounterServiceProfile : Profile
{
    public CounterServiceProfile()
    {
        CreateMap<Models.Models.CounterServices, CounterServicesDto>()
            .ForMember(dest => dest.CounterId, opt => opt.MapFrom(src => src.CounterId))
            .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
            .ForMember(dest => dest.CounterName, opt => opt.MapFrom(src => src.Counter!.CounterName))
            .ForMember(dest => dest.serviceName, opt => opt.MapFrom(src => src.Service!.ServiceName))
            .ReverseMap();
        CreateMap<Models.Models.CounterServices, CounterServicesAddDto>().ReverseMap();
        CreateMap<Models.Models.CounterServices, CounterServicesUpdateDto>().ReverseMap();
    }
}
