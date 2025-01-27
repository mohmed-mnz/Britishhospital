using ApiContracts;
using ApiContracts.Display;

namespace BussinesLayer.Interfaces;

public interface IDisplayServices
{
    Task<GResponse<DisplayDto>> GetDisplaybyId(int id);
    Task<GResponse<List<DisplayDto>>> GetAllDisplays();
    Task<GResponse<DisplayDto>> AddDisplay(DisplayAddDto model);
    Task<GResponse<DisplayDto>> UpdateDisplay(DisplayUpdateDto model);
    Task<GResponse<bool>> DeleteDisplay(int id);
    Task<GResponse<IEnumerable<DisplayDto>>> GetAllBasedonorgid(int orgid);
    Task<GResponse<DisplayDetailsDto>>GetDisplayDetails(int displayId);
}
