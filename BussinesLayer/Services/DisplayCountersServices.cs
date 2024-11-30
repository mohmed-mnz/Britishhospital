using ApiContracts;
using ApiContracts.DisplayCounter;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BussinesLayer.Services;

public class DisplayCountersServices : IDisplayCounterServices
{
    private readonly IDisplayCountersRepository _repository;
    private readonly IMapper _mapper;

    public DisplayCountersServices(IDisplayCountersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GResponse<DisplayCountersDto>> AddCounterAsync(DisplayCountersAddDto counter)
    {
        var counterModel = _mapper.Map<Models.Models.DisplayCounters>(counter);
        await _repository.InsertAsync(counterModel);
        await _repository.Commit();
        counterModel = await _repository.Where(x => x.Id == counterModel.Id)!.Include(x => x.Counter).Include(x => x.Display).FirstOrDefaultAsync();
        var counterDto = _mapper.Map<DisplayCountersDto>(counterModel);
        return GResponse<DisplayCountersDto>.CreateSuccess(counterDto);
    }

    public async Task<GResponse<bool>> DeleteCounterAsync(int id)
    {
        var counter = await _repository.Where(x => x.Id == id)!.FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        _repository.Delete(counter);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }

    public async Task<GResponse<DisplayCountersDto>> GetCounterAsync(int id)
    {
        var counter = await _repository.Where(x => x.Id == id)!.Include(x => x.Counter).Include(x => x.Display).FirstOrDefaultAsync();
        if (counter == null)
        {
            throw new ApplicationException("Counter not found");
        }
        var counterDto = _mapper.Map<DisplayCountersDto>(counter);
        return GResponse<DisplayCountersDto>.CreateSuccess(counterDto);
    }

    public async Task<GResponse<IEnumerable<DisplayCountersDto>>> GetCountersAsync()
    {
        var counters = await _repository.AsQueryable()!.Include(x => x.Counter).Include(x => x.Display).AsSplitQuery().ToListAsync();

        var countersDto = _mapper.Map<IEnumerable<DisplayCountersDto>>(counters);
        return GResponse<IEnumerable<DisplayCountersDto>>.CreateSuccess(countersDto);
    }

    public async Task<GResponse<IEnumerable<DisplayCountersDto>>> GetCountersByDisplayAsync(int displayId)
    {
        var counters = await _repository.Where(x => x.DisplayId == displayId)!.Include(x => x.Counter).Include(x => x.Display).AsSplitQuery().ToListAsync();
        var countersDto = _mapper.Map<IEnumerable<DisplayCountersDto>>(counters);
        return GResponse<IEnumerable<DisplayCountersDto>>.CreateSuccess(countersDto);
    }

    public async Task<GResponse<DisplayCountersDto>> UpdateCounterAsync(DisplayCountersUpdateDto counter)
    {
        var counters = await _repository.Where(x => x.Id == counter.Id)!.Include(x => x.Counter).Include(x => x.Display).AsSplitQuery().FirstOrDefaultAsync();
        if (counters == null)
        {
            throw new ApplicationException("Counter not found");
        }
        counters = _mapper.Map(counter, counters);
        await _repository.Commit();
        return GResponse<DisplayCountersDto>.CreateSuccess(_mapper.Map<DisplayCountersDto>(counters));
    }
}
