using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class GroupUserRepository:Repository<GroupUser>, IGroupUserRepository
{
    public GroupUserRepository(BritshHosbitalContext context) : base(context)
    {
        
    }
}
