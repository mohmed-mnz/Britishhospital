using AutoMapper;

namespace BussinesLayer.Mapping.DisplayCounter;

public class DisplayCounterProfile : Profile
{
    public DisplayCounterProfile()
    {
        CreateMap<Models.Models.DisplayCounters, ApiContracts.DisplayCounter.DisplayCountersDto>()
            .ForMember(destinationMember => destinationMember.DisplayName, memberOptions => memberOptions.MapFrom(source => source.Display!.DisplayName))
            .ForMember(destinationMember => destinationMember.CounterName, memberOptions => memberOptions.MapFrom(source => source.Counter!.CounterName))
            .ReverseMap();
        CreateMap<ApiContracts.DisplayCounter.DisplayCountersAddDto, Models.Models.DisplayCounters>()
            .ReverseMap();
        CreateMap<ApiContracts.DisplayCounter.DisplayCountersUpdateDto, Models.Models.DisplayCounters>()
            .ReverseMap();
    }
}
