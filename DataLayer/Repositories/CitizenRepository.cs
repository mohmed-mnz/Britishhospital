using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class CitizenRepository : Repository<Citizen>, ICitizenRepository
{
    public CitizenRepository(BritshHosbitalContext context) : base(context)
    {

    }
}
