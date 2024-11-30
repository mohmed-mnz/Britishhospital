using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class DisplayAdvertsRepository : Repository<DisplayAdverts>, IDisplayAdvertsRepository
{
    public DisplayAdvertsRepository(BritshHosbitalContext context) : base(context)
    {

    }
}
