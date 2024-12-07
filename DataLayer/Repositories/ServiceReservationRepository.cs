using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class ServiceReservationRepository:Repository<ServiceReservation>, IServiceReservationRepository
{
    public ServiceReservationRepository(BritshHosbitalContext context): base(context)
    {
        
    }
}
