using ApiContracts.DisplayCounter;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.DisplayCounter;

public class DisplayCounterProfile : Profile
{
    public DisplayCounterProfile()
    {
        CreateMap<DisplayCounters, DisplayCountersDto>()
            .ForMember(destinationMember => destinationMember.DisplayName, memberOptions => memberOptions.MapFrom(source => source.Display!.DisplayName))
            .ForMember(destinationMember => destinationMember.CounterName, memberOptions => memberOptions.MapFrom(source => source.Counter!.CounterName))
            .ReverseMap();
        CreateMap<DisplayCountersAddDto, DisplayCounters>()
            .ReverseMap();
        CreateMap<DisplayCountersUpdateDto, DisplayCounters>()
            .ReverseMap();


        CreateMap<DisplayCounters, DisplayCountersTinyDto>()
          .ForMember(dest => dest.CounterId, opt => opt.MapFrom(src => src.CounterId))
          .ForMember(dest => dest.CounterName, opt => opt.MapFrom(src => src.Counter!.CounterName));
    }
}
