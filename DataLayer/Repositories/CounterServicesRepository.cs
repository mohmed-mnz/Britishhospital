using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class CounterServicesRepository:Repository<CounterServices>, ICounterServicesRepository
{
    public CounterServicesRepository(BritshHosbitalContext context):base(context)
    {
        
    }
}
