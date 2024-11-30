using ApiContracts;
using ApiContracts.CounterService;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BussinesLayer.Services;

public class CounterServicesServices(ICounterServicesRepository counterServicesRepository, IMapper mapper) : ICounterServicesServices
{
    public async Task<GResponse<CounterServicesDto>> CreateAsync(CounterServicesAddDto counterServicesDto)
    {
        var counterServices = mapper.Map<Models.Models.CounterServices>(counterServicesDto);
        await counterServicesRepository.InsertAsync(counterServices);
        await counterServicesRepository.Commit();
        counterServices = await counterServicesRepository.AsQueryable().Include(x => x.Counter).Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == counterServices.Id);
        var result = mapper.Map<CounterServicesDto>(counterServices);
        return GResponse<CounterServicesDto>.CreateSuccess(result);
    }

    public async Task<GResponse<bool>> DeleteAsync(int id)
    {
        var counterServices = await counterServicesRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        if (counterServices == null)
        {
            throw new ApplicationException("CounterServices not found");
        }
        counterServicesRepository.Delete(counterServices);
        await counterServicesRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<IEnumerable<CounterServicesDto>>> GetAsync()
    {
        var counterServices = await counterServicesRepository.AsQueryable().Include(x => x.Counter).Include(x => x.Service).ToListAsync();
        var result = mapper.Map<IEnumerable<CounterServicesDto>>(counterServices);
        return GResponse<IEnumerable<CounterServicesDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<IEnumerable<CounterServicesDto>>> GetbasedonCounterIdAsync(int counterId)
    {
        var counterServices = await counterServicesRepository.AsQueryable().Include(x => x.Counter).Include(x => x.Service).Where(x => x.CounterId == counterId).ToListAsync();
        var result = mapper.Map<IEnumerable<CounterServicesDto>>(counterServices);
        return GResponse<IEnumerable<CounterServicesDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<CounterServicesDto>> GetByIdAsync(int id)
    {
        var counterServices = await counterServicesRepository.AsQueryable().Include(x => x.Counter).Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == id);
        if (counterServices == null)
        {
            throw new ApplicationException("CounterServices not found");
        }
        var result = mapper.Map<CounterServicesDto>(counterServices);
        return GResponse<CounterServicesDto>.CreateSuccess(result);
    }

    public async Task<GResponse<CounterServicesDto>> UpdateAsync(CounterServicesUpdateDto counterServicesDto)
    {
        var counterServices = await counterServicesRepository.AsQueryable().Include(x => x.Counter).Include(x => x.Service).FirstOrDefaultAsync(x => x.Id == counterServicesDto.Id);
        if (counterServices == null)
        {
            throw new ApplicationException("CounterServices not found");
        }
        counterServices = mapper.Map(counterServicesDto, counterServices);
        await counterServicesRepository.Commit();
        var result = mapper.Map<CounterServicesDto>(counterServices);
        return GResponse<CounterServicesDto>.CreateSuccess(result);
    }
}

