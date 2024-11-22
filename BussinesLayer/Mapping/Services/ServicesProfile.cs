using ApiContracts.Service;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Services;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ReverseMap();
        CreateMap<ServiceAddDto,Service>()
            .ReverseMap();
        CreateMap<ServiceUpdateDto, Service>()
            .ReverseMap();
    }
}
