using ApiContracts;
using ApiContracts.Employee;
using AutoMapper;
using BussinesLayer.Interfaces;
using BussinesLayer.Interfaces.Token;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using SharedConfig;

namespace BussinesLayer.Services;

public class EmployeeServices(IEmployeeRepository _repository, IMapper _mapper, ICitizenRepository citizenRepository, ITokenService _tokenService, IPresistanceService _presistanceService, AppConfiguration _appConfig) : IEmployeeServices
{
    public async Task<GResponse<bool>> DeleteEmployeeAsync(int id)
    {
        var emp = await _repository.FindAsync(id)!;
        if (emp == null)
        {
            throw new ApplicationException("emp Not Found");
        }
        _repository.Delete(emp);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<EmployeeDto>> GetEmployeeByIdAsync(int id)
    {
        var employee = await _repository.FindAsync(id)!;
        if (employee == null)
            throw new ApplicationException("emp not found");
        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return GResponse<EmployeeDto>.CreateSuccess(employeeDto);
    }

    public async Task<GResponse<IEnumerable<EmployeeDto>>> GetEmployeeByOrgIdListAsync(int orgId)
    {
        var employees = await _repository.Where(x => x.Orgid == orgId)!.Include(x => x.Org).Include(z => z.Citizen).AsSplitQuery().ToListAsync();
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return GResponse<IEnumerable<EmployeeDto>>.CreateSuccess(employeeDtos);
    }

    public async Task<GResponse<IEnumerable<EmployeeDto>>> GetEmployeeListAsync()
    {
        var employees = await _repository.AsQueryable()!.Include(x => x.Org).Include(z => z.Citizen).AsSplitQuery().ToListAsync();
        var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        return GResponse<IEnumerable<EmployeeDto>>.CreateSuccess(employeeDtos);
    }

    public async Task<GResponse<EmployeeLoginDto>> Login(EmpLogin loginDto)
    {
        var user = await _repository.Where(u => u.Username == loginDto.Username)!.Include(c => c.Citizen).Include(o => o.Org).Include(x => x.GroupUser).ThenInclude(g => g.Group).FirstOrDefaultAsync();

        if (user == null)
            throw new ApplicationException("UserName or Password is not valid");

        if (user.IsActive == false)
            throw new ApplicationException("UserName or Password is not valid or Not Active User");

        if (isValidPassword(loginDto.Password, user.Password))
        {
            Guid tokenId = Guid.NewGuid();

            Dictionary<string, string> claims = new()
        {
                { "EmpId", user.Id.ToString() },
                { "CitizenId", user.Citizenid.ToString() ??""},
                {"GroupUser" , string.Join(", ", user.GroupUser.Select(x => x.Group.GroupName)) }
        };
            var token = _tokenService.Create(claims, _appConfig.Jwt!.ExpirytimeinMinutes);
            _presistanceService.Set($"Token_{tokenId}", token, TimeSpan.FromMinutes(_appConfig.Jwt.ExpirytimeinMinutes * 24 * 60));

            Dictionary<string, string> tokenIdClaims = new()
                    {
                        { "TokenId", tokenId.ToString() }
                    };

            var emp = new EmployeeLoginDto();
            emp.CTZNID = user.Citizenid ?? 0;
            emp.EMPID = user.Id;
            emp.Token = _tokenService.Create(tokenIdClaims, _appConfig.Jwt!.ExpirytimeinMinutes * 24 * 60);
            emp.UserName = user.Username;
            emp.ORGID = user.Orgid ?? 0;
            emp.NID = user.Citizen!.Nid!;
            emp.OrgName = user.Org!.OrgName;
            emp.ExpirytimeinMinutes = _appConfig.Jwt.ExpirytimeinMinutes;
            emp.Roles = user.GroupUser.Select(x => x.Group!.GroupName).ToList()!;
            return GResponse<EmployeeLoginDto>.CreateSuccess(emp);
        }
        else
        {

            return GResponse<EmployeeLoginDto>.CreateFailure("407", "Password is not valid");
        }
    }

    public async Task<GResponse<bool>> RegestierEmployee(EmployeeAddDto employeeAddDto)
    {
        var citizenexist = await _repository.AsQueryable()!.FirstOrDefaultAsync(x => x.Citizen!.Nid == employeeAddDto.Nid);
        if (citizenexist != null)
        {
            var emp = await _repository.AsQueryable()!.FirstOrDefaultAsync(x => x.Citizen!.Nid == employeeAddDto.Nid);
            if (emp == null)
            {
                emp!.Username = employeeAddDto.Username;
                emp.Password = await _repository.EncryptPassword(employeeAddDto.Password);
                emp.Citizenid = citizenexist.Citizenid;
                emp.Orgid = employeeAddDto.Orgid;
                emp.IsActive = true;
                await _repository.Commit();
                return GResponse<bool>.CreateSuccess(true);
            }
            else
            {
                return GResponse<bool>.CreateFailure("402", "Employee  already Exist");
            }
        }
        else
        {
            var citizen = new Citizen
            {
                Nid = employeeAddDto.Nid,
                Name = employeeAddDto.Name,
                Mobile = employeeAddDto.MobileNumber!,
                Address = employeeAddDto.Address,
                Email = employeeAddDto.Email,
                Sex = employeeAddDto.gender
            };
            await citizenRepository.InsertAsync(citizen);
            await citizenRepository.Commit();
            var emp = new Employee
            {
                Username = employeeAddDto.Username,
                Password = await _repository.EncryptPassword(employeeAddDto.Password),
                Citizenid = citizen.CitizenId,
                Orgid = employeeAddDto.Orgid,
                IsActive = true
            };
            await _repository.InsertAsync(emp);
            await _repository.Commit();
            return GResponse<bool>.CreateSuccess(true);

        }

    }
    private bool isValidPassword(string password, string computedHash)
    {
        string hashedPassword = _repository.EncryptPassword(password).Result;
        return (hashedPassword == computedHash);
    }

    public async Task<GResponse<bool>> SetEmployeeAsNotActive(int empid)
    {
        var emp = await _repository.AsQueryable()!.FirstOrDefaultAsync(x => x.Id == empid);
        if (emp == null)
        {
            return GResponse<bool>.CreateFailure("404", "Employee Not Found");
        }
        emp.IsActive = !emp.IsActive;
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<bool>> UpdateEmployeeData(EmployeeUpdateDataDto employeeUpdateDataDto)
    {
        var emp = await _repository.AsQueryable()!.Where(x => x.Id == employeeUpdateDataDto.Id).Include(x => x.Citizen).FirstOrDefaultAsync();
        if (emp == null)
        {
            return GResponse<bool>.CreateFailure("404", "Employee Not Found");
        }
        emp.Citizen!.Mobile = employeeUpdateDataDto.Mobile!;
        emp.Citizen.Address = employeeUpdateDataDto.address;
        emp.Citizen.Email = employeeUpdateDataDto.email;
        emp.Citizenid = emp.Citizen.CitizenId;
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);

    }
}
