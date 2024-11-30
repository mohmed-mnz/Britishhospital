using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class CounterRepository : Repository<Counters>, ICounterRepository
{
    public CounterRepository(BritshHosbitalContext context) : base(context)
    {

    }
}
