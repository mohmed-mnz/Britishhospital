using ApiContracts;
using ApiContracts.Counters;

namespace BussinesLayer.Interfaces;

public interface ICountersServices
{
    Task<GResponse<CountersDto>> GetCounterbyId(int id);
    Task<GResponse<List<CountersDto>>> GetAllCounters();
    Task<GResponse<CountersDto>> AddCounter(CountersAddDto model);
    Task<GResponse<CountersDto>> UpdateCounter(CountersUpdateDto model);
    Task<GResponse<bool>> DeleteCounter(int id);
    Task<GResponse<IEnumerable<CountersDto>>> GetAllBasedonorgid(int orgid);

}
