using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class DisplayCountersRepository:Repository<DisplayCounters>, IDisplayCountersRepository
{
    public DisplayCountersRepository(BritshHosbitalContext context):base(context)
    {
        
    }
}
