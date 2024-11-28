using ApiContracts;
using ApiContracts.DisplayCounter;

namespace BussinesLayer.Interfaces;

public interface IDisplayCounterServices
{
    Task<GResponse<IEnumerable<DisplayCountersDto>>> GetCountersAsync();
    Task<GResponse<DisplayCountersDto>> GetCounterAsync(int id);
    Task<GResponse<DisplayCountersDto>> AddCounterAsync(DisplayCountersAddDto counter);
    Task<GResponse<DisplayCountersDto>> UpdateCounterAsync(DisplayCountersUpdateDto counter);
    Task<GResponse<bool>> DeleteCounterAsync(int id);
    Task<GResponse<IEnumerable<DisplayCountersDto>>> GetCountersByDisplayAsync(int displayId);
}
