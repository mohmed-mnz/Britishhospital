using ApiContracts;
using ApiContracts.Display;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace BussinesLayer.Services;

public class DisplayServices(IDisplayRepository displayRepository, IMapper mapper) : IDisplayServices
{
    public async Task<GResponse<DisplayDto>> AddDisplay(DisplayAddDto model)
    {
        var display = mapper.Map<Display>(model);
        await displayRepository.InsertAsync(display);
        await displayRepository.Commit();
        var result = mapper.Map<DisplayDto>(display);
        return GResponse<DisplayDto>.CreateSuccess(result);



    }

    public async Task<GResponse<bool>> DeleteDisplay(int id)
    {
        var display = await displayRepository.Where(x => x.DisplayId == id)!.FirstOrDefaultAsync();
        if (display == null)
        {
            throw new ApplicationException("Display not found");
        }
        displayRepository.Delete(display);
        await displayRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<IEnumerable<DisplayDto>>> GetAllBasedonorgid(int orgid)
    {
        var displays =await displayRepository.AsQueryable().Where(x => x.Orgid == orgid).Include(x => x.Org).ToListAsync();
        var result = mapper.Map<List<DisplayDto>>(displays);
        return GResponse<IEnumerable<DisplayDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<List<DisplayDto>>> GetAllDisplays()
    {
        var
            displays =await displayRepository.AsQueryable().Include(x => x.Org).ToListAsync();
        var result = mapper.Map<List<DisplayDto>>(displays);
        return GResponse<List<DisplayDto>>.CreateSuccess(result);
    }

    public async Task<GResponse<DisplayDto>> GetDisplaybyId(int id)
    {
        var display =await displayRepository.Where(x => x.DisplayId == id)!.Include(x => x.Org).FirstOrDefaultAsync();
        if (display == null)
        {
            throw new ApplicationException("Display not found");
        }

        var result = mapper.Map<DisplayDto>(display);
        return GResponse<DisplayDto>.CreateSuccess(result);
    }

    public async Task<GResponse<DisplayDto>> UpdateDisplay(DisplayUpdateDto model)
    {
        var display = await displayRepository.Where(x => x.DisplayId == model.DisplayId)!.Include(x => x.Org).FirstOrDefaultAsync();
        if (display == null)
        {
            throw new ApplicationException("Display not found");
        }
        display = mapper.Map(model, display);
        await displayRepository.Commit();
        var result = mapper.Map<DisplayDto>(display);
        return GResponse<DisplayDto>.CreateSuccess(result);
    }
}
