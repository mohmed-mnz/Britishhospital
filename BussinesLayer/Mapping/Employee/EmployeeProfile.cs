using ApiContracts.Employee;
using AutoMapper;
using Models.Models;

namespace BussinesLayer.Mapping.Employee;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Models.Models.Employee, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeAddDto, Models.Models.Employee>().ReverseMap();
        CreateMap<EmployeeUpdateDataDto, Models.Models.Employee>().ReverseMap();
    }
}
