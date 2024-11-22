using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Models.Models;

namespace DataLayer.Repositories;

public class EmployeeRepository :Repository<Employee>,IEmployeeRepository
{
    public EmployeeRepository(BritshHosbitalContext context):base(context)
    {
        
    }
}
