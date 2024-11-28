using ApiContracts;
using ApiContracts.DisplayAdvert;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BussinesLayer.Services;

public class DisplayAdvertsServices : IDisplayAdvertsServices
{
    private readonly IDisplayAdvertsRepository _repository;
    private readonly IMapper _mapper;

    public DisplayAdvertsServices(IDisplayAdvertsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GResponse<DisplayAdvertsDto>> AddAdvertAsync(DisplayAdvertsAddDto advert)
    {
        var advertModel = _mapper.Map<Models.Models.DisplayAdverts>(advert);
        await _repository.InsertAsync(advertModel);
        await _repository.Commit();
        advertModel= await _repository.Where(x => x.Id == advertModel.Id)!.Include(x => x.Advert).Include(x => x.Display).FirstOrDefaultAsync();
        var advertDto = _mapper.Map<DisplayAdvertsDto>(advertModel);
        return  GResponse<DisplayAdvertsDto>.CreateSuccess(advertDto);

    }

    public async Task<GResponse<bool>> DeleteAdvertAsync(int id)
    {
        var advert =await _repository.Where(x => x.Id == id)!.FirstOrDefaultAsync();
        if (advert == null)
        {
            throw new ApplicationException("Advert not found");
        }
        _repository.Delete(advert);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<DisplayAdvertsDto>> GetAdvertAsync(int id)
    {
        var advert =await _repository.Where(x => x.Id == id)!.Include(x => x.Advert).Include(x => x.Display).FirstOrDefaultAsync();
        if (advert == null)
        {
            throw new ApplicationException("Advert not found");
        }
        var advertDto = _mapper.Map<DisplayAdvertsDto>(advert);
        return GResponse<DisplayAdvertsDto>.CreateSuccess(advertDto);
    }

    public async Task<GResponse<IEnumerable<DisplayAdvertsDto>>> GetAdvertsAsync()
    {
        var adverts =await _repository.AsQueryable()!.Include(x => x.Advert).Include(x => x.Display).AsSplitQuery().ToListAsync();
        var advertsDto = _mapper.Map<IEnumerable<DisplayAdvertsDto>>(adverts);
        return GResponse<IEnumerable<DisplayAdvertsDto>>.CreateSuccess(advertsDto);
    }

    public async Task<GResponse<IEnumerable<DisplayAdvertsDto>>> GetAdvertsByDisplayAsync(int displayId)
    {
        var adverts =await _repository.Where(x => x.DisplayId == displayId)!.Include(x => x.Advert).Include(x => x.Display).AsSplitQuery().ToListAsync();
        var advertsDto = _mapper.Map<IEnumerable<DisplayAdvertsDto>>(adverts);
        return GResponse<IEnumerable<DisplayAdvertsDto>>.CreateSuccess(advertsDto);
    }

    public async Task<GResponse<DisplayAdvertsDto>> UpdateAdvertAsync(DisplayAdvertsUpdateDto advert)
    {
        var adverts = await _repository.Where(x => x.Id == advert.Id)!.FirstOrDefaultAsync();
        if (adverts == null)
        {
            throw new ApplicationException("Advert not found");
        }
        adverts = _mapper.Map(advert, adverts);
        await _repository.Commit();
        return GResponse<DisplayAdvertsDto>.CreateSuccess(_mapper.Map<DisplayAdvertsDto>(adverts));
    }
}
