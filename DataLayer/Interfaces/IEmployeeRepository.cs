using DataAccessLayer.Interfaces;
using Models.Models;

namespace DataLayer.Interfaces;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<string> EncryptPassword(string Password);
}
