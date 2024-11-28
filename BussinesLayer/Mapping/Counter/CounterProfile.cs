using ApiContracts.Counters;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Counter;

public class CounterProfile : Profile
{
    public CounterProfile()
    {
        CreateMap<CountersAddDto, Counters>()
            .ReverseMap();
        CreateMap<CountersUpdateDto, Counters>()
            .ReverseMap();
        CreateMap<Counters, CountersDto>()
            .ForMember(dest => dest.empname, opt => opt.MapFrom(src => src.Emp!.Citizen!.Name))
            .ForMember(destinationMember: dest => dest.Orgname, memberOptions: opt => opt.MapFrom(src => src.Org!.OrgName))
            .ReverseMap();

    }
}
