using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class BookingSettingOrgRepository : Repository<BookingSettingOrg>, IBookingSettingOrgRepository
{
    public BookingSettingOrgRepository(BritshHosbitalContext context) : base(context)
    {

    }
}
