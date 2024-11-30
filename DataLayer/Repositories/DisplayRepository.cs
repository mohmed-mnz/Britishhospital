using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class DisplayRepository : Repository<Display>, IDisplayRepository
{
    public DisplayRepository(BritshHosbitalContext context) : base(context)
    {

    }
}
