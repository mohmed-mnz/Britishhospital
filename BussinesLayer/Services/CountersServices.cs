using ApiContracts;
using ApiContracts.Counters;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace BussinesLayer.Services;

public class CountersServices(ICounterRepository counterRepository, IMapper mapper) : ICountersServices
{
    public async Task<GResponse<CountersDto>> AddCounter(CountersAddDto model)
    {
        var counter = mapper.Map<Counters>(model);
        await counterRepository.InsertAsync(counter);
        await counterRepository.Commit();
        counter = await counterRepository.Where(x => x.CounterId == counter.CounterId)!.Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(o => o.Org).AsSplitQuery().FirstOrDefaultAsync();
        var result = mapper.Map<CountersDto>(counter);
        return GResponse<CountersDto>.CreateSuccess(result);


    }

    public async Task<GResponse<CountersDto>> assignemployeetocounter(CountersUpdateToAssignEmployeetoCounterDto countersUpdateTo)
    {
        var counter =await counterRepository.Where(x => x.CounterId == countersUpdateTo.CounterId)!.AsSplitQuery().FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counter.empid = countersUpdateTo.EmpId;
        await counterRepository.Commit();
        counter =await counterRepository.AsQueryable().Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(o => o.Org).FirstOrDefaultAsync(x => x.CounterId == countersUpdateTo.CounterId);
        var result = mapper.Map<CountersDto>(counter);
        
        result.empname = counter.Emp!.Citizen!.Name;
        result.Orgname = counter.Org!.OrgName;

        return GResponse<CountersDto>.CreateSuccess(result);
    }

    public async Task<GResponse<bool>> DeleteCounter(int id)
    {
        var counter = await counterRepository.Where(x => x.CounterId == id)!.FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counterRepository.Delete(counter);
        await counterRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<bool>> DeleteEmployeeFromCounter(int counterid)
    {
        var counter = await counterRepository.Where(x => x.CounterId == counterid)!.FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counter.empid = null;
        await counterRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }

    public async Task<GResponse<IEnumerable<CountersDto>>> GetAllBasedonorgid(int orgid)
    {
        var counters = await counterRepository.AsQueryable().Where(x => x.Orgid == orgid).Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(o => o.Org).Include(x=>x.CounterServices).ThenInclude(cs => cs.Service).AsSplitQuery().ToListAsync();
        var result = mapper.Map<List<CountersDto>>(counters);
        foreach (var item in result)
        {
            item.Orgname = counters.FirstOrDefault(x => x.Orgid == item.Orgid)?.Org!.OrgName;
            item.empname = counters.FirstOrDefault(x => x.empid == item.empid)?.Emp!.Citizen!.Name;
            item.CounterServices= item.CounterServices;

        }
        return GResponse<IEnumerable<CountersDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<List<CountersDto>>> GetAllCounters()
    {
        var counters = await counterRepository.AsQueryable().Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(o => o.Org).Include(x => x.CounterServices).ThenInclude(cs => cs.Service).AsSplitQuery().ToListAsync();
        var result = mapper.Map<List<CountersDto>>(counters);
        return GResponse<List<CountersDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<CountersDto>> GetCounterbyId(int id)
    {
        var counter = await counterRepository.Where(x => x.CounterId == id)!.Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(x=>x.Org).Include(x => x.CounterServices).ThenInclude(cs => cs.Service).FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        var result = mapper.Map<CountersDto>(counter);
        return GResponse<CountersDto>.CreateSuccess(result);

    }

    public async Task<GResponse<bool>> setthecounternotactive(int counterid)
    {
        var counter =await counterRepository.Where(x => x.CounterId == counterid)!.FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counter.IsActive =! counter.IsActive;
        await counterRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<CountersDto>> UpdateCounter(CountersUpdateDto model)
    {
        var counter = await counterRepository.Where(x => x.CounterId == model.CounterId)!.Include(x => x.Emp).ThenInclude(y => y.Citizen).Include(o => o.Org).Include(x => x.CounterServices).ThenInclude(cs => cs.Service).AsSplitQuery().FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counter = mapper.Map(model, counter);
        await counterRepository.Commit();
        var result = mapper.Map<CountersDto>(counter);
        return GResponse<CountersDto>.CreateSuccess(result);
    }
}

