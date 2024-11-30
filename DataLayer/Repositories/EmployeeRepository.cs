using DataAccessLayer.Repositories;
using DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace DataLayer.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    private readonly BritshHosbitalContext _context;

    public EmployeeRepository(BritshHosbitalContext context) : base(context)
    {
        _context = context;

    }

    public async Task<string> EncryptPassword(string password)
    {
        var encryptedPasswordParam = new SqlParameter
        {
            ParameterName = "@EncryptedPassword",
            SqlDbType = System.Data.SqlDbType.NVarChar,
            Size = -1,
            Direction = System.Data.ParameterDirection.Output
        };

        var passwordParam = new SqlParameter
        {
            ParameterName = "@Password",
            SqlDbType = System.Data.SqlDbType.NVarChar,
            Value = password
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC EncryptPassword @Password, @EncryptedPassword OUTPUT",
            passwordParam, encryptedPasswordParam
        );

        return encryptedPasswordParam.Value?.ToString()!;
    }

}
