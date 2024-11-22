using ApiContracts;
using ApiContracts.Service;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;

namespace BussinesLayer.Services;

public class ServicesServices(IServiceRepository _repository, IMapper _mapper) : IServicesServices
{
    public async Task<GResponse<ServiceDto>> AddServiceAsync(ServiceAddDto serviceAddDto)
    {
        var service = _mapper.Map<Service>(serviceAddDto);
        await _repository.InsertAsync(service);
        await _repository.Commit();
        var result = _mapper.Map<ServiceDto>(service);
        return GResponse<ServiceDto>.CreateSuccess(result);
    }

    public async Task<GResponse<bool>> DeleteServiceAsync(int id)
    {
        var service =await _repository.FindAsync(id)!;
        if (service == null)
        {
            throw new ApplicationException("Service not found");
        }
        _repository.Delete(service);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }

    public async Task<GResponse<ServiceDto>> GetServiceByIdAsync(int id)
    {
        var service =await _repository.FindAsync(id)!;
        if (service == null)
        {
            throw new ApplicationException("Service not found");
        }
        var result = _mapper.Map<ServiceDto>(service);
        return GResponse<ServiceDto>.CreateSuccess(result);
    }

    public async Task<GResponse<IEnumerable<ServiceDto>>> GetServicesAsync()
    {
        var services =await _repository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<ServiceDto>>(services);
        return GResponse<IEnumerable<ServiceDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<ServiceDto>> UpdateServiceAsync(ServiceUpdateDto serviceUpdateDto)
    {
        var service = await _repository.FindAsync(serviceUpdateDto.Id)!;
        if (service == null)
        {
            throw new ApplicationException("Service not found");
        }
        service= _mapper.Map(serviceUpdateDto, service);
        await _repository.Commit();
        var result = _mapper.Map<ServiceDto>(service);
        return GResponse<ServiceDto>.CreateSuccess(result);


    }
}
