using ApiContracts;
using ApiContracts.Employee;

namespace BussinesLayer.Interfaces;

public interface IEmployeeServices
{
    Task<GResponse<IEnumerable<EmployeeDto>>> GetEmployeeListAsync();
    Task<GResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
    Task<GResponse<bool>> DeleteEmployeeAsync(int id);
    Task<GResponse<bool>> RegestierEmployee(EmployeeAddDto employeeAddDto);
    Task<GResponse<bool>> UpdateEmployeeData(EmployeeUpdateDataDto employeeUpdateDataDto);
    Task<GResponse<IEnumerable<EmployeeDto>>> GetEmployeeByOrgIdListAsync(int orgId);
    Task<GResponse<bool>> SetEmployeeAsNotActive(int empid);
    Task<GResponse<EmployeeLoginDto>> Login(EmpLogin loginDto);
}
