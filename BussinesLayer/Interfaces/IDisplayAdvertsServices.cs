using ApiContracts;
using ApiContracts.DisplayAdvert;

namespace BussinesLayer.Interfaces;

public interface IDisplayAdvertsServices
{
    Task<GResponse<IEnumerable<DisplayAdvertsDto>>> GetAdvertsAsync();
    Task<GResponse<DisplayAdvertsDto>> GetAdvertAsync(int id);
    Task<GResponse<DisplayAdvertsDto>> AddAdvertAsync(DisplayAdvertsAddDto advert);
    Task<GResponse<DisplayAdvertsDto>> UpdateAdvertAsync(DisplayAdvertsUpdateDto advert);
    Task<GResponse<bool>> DeleteAdvertAsync(int id);
    Task<GResponse<IEnumerable<DisplayAdvertsDto>>> GetAdvertsByDisplayAsync(int displayId);
}
