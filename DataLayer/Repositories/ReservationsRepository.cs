using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class ReservationsRepository:Repository<Reservations>, IReservationsRepository
{
    public ReservationsRepository(BritshHosbitalContext context) : base(context)
    {
        
    }
}
