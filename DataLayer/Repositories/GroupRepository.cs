using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class GroupRepository:Repository<Group>, IGroupRepository
{
    public GroupRepository(BritshHosbitalContext context) : base(context)
    {
        
    }
}
