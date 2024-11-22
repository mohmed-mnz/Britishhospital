using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class AdvertismentRepository:Repository<Advertisment>, IAdvertismentRepository
{
    public AdvertismentRepository(BritshHosbitalContext context ):base(context)
    {
        
    }
}
