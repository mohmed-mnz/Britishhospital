using ApiContracts;
using ApiContracts.OrgSetteing;

namespace BussinesLayer.Interfaces;

public interface IBookingSettingOrgServices
{
    Task<GResponse<IEnumerable<BookingSettingOrgDto>>> GetBookingSettingOrgsAsync(int orgid);
    Task<GResponse<BookingSettingOrgDto>> GetBookingSettingOrgAsync(int id);
    Task<GResponse<BookingSettingOrgDto>> AddBookingSettingOrgAsync(BookingSettingOrgAddDto bookingSettingOrgAddDto);
    Task<GResponse<BookingSettingOrgDto>> UpdateBookingSettingOrgAsync(BookingSettingOrgUpdateDto bookingSettingOrgUpdateDto);
    Task<GResponse<bool>> DeleteBookingSettingOrgAsync(int id);

}
