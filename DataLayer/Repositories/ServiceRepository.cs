using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class ServiceRepository:Repository<Service>, IServiceRepository
{
    public ServiceRepository(BritshHosbitalContext context):base(context)
    {
        
    }
}
