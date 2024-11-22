using ApiContracts;
using ApiContracts.Advertisment;

namespace BussinesLayer.Interface;

public interface IAdvertismentServices
{
    Task<GResponse<AdvertismentDto>> GetAdvertismentById(int id);
    Task<GResponse<List<AdvertismentDto>>> GetAllAdvertisments();
    Task<GResponse<AdvertismentDto>> AddAdvertisment(AdvertismentAddDto model);
    Task<GResponse<AdvertismentDto>> UpdateAdvertisment(AdvertismentUpdateDto model);
    Task<GResponse<bool>> DeleteAdvertisment(int id);
}
