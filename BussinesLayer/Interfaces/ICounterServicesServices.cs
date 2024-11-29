using ApiContracts;
using ApiContracts.CounterService;

namespace BussinesLayer.Interfaces;

public interface ICounterServicesServices
{
    Task<GResponse<IEnumerable<CounterServicesDto>>> GetbasedonCounterIdAsync(int counterId);
    Task<GResponse<IEnumerable<CounterServicesDto>>> GetAsync();
    Task<GResponse<CounterServicesDto>> GetByIdAsync(int id);
    Task<GResponse<CounterServicesDto>> CreateAsync(CounterServicesAddDto counterServicesDto);
    Task<GResponse<CounterServicesDto>> UpdateAsync(CounterServicesUpdateDto counterServicesDto);
    Task<GResponse<bool>> DeleteAsync(int id);
}
