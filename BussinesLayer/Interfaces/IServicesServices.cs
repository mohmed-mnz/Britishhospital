using ApiContracts;
using ApiContracts.Service;

namespace BussinesLayer.Interfaces;

public interface IServicesServices
{
    Task<GResponse<IEnumerable<ServiceDto>>> GetServicesAsync();
    Task<GResponse<ServiceDto>> GetServiceByIdAsync(int id);
    Task<GResponse<ServiceDto>> AddServiceAsync(ServiceAddDto serviceAddDto);
    Task<GResponse<ServiceDto>> UpdateServiceAsync(ServiceUpdateDto serviceUpdateDto);
    Task<GResponse<bool>> DeleteServiceAsync(int id);
}
