using ApiContracts;
using ApiContracts.groupusers;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace BussinesLayer.Services;

public class GroupUserServices(IGroupUserRepository groupUserRepository, IMapper mapper) : IGroupUserServices
{
    public async Task<GResponse<GroupUserDto>> AddGroupUserAsync(GroupUserAddDto groupUserAddDto)
    {
        var groupUser = mapper.Map<GroupUser>(groupUserAddDto);
        await groupUserRepository.InsertAsync(groupUser);
        await groupUserRepository.Commit();
        groupUser = await groupUserRepository.AsQueryable().Include(x => x.Group).Include(z => z.Emp.Citizen).FirstOrDefaultAsync(x => x.Id == groupUser.Id);
        var groupUserDto = mapper.Map<GroupUserDto>(groupUser);
        return GResponse<GroupUserDto>.CreateSuccess(groupUserDto);
    }

    public async Task<GResponse<bool>> DeleteGroupUserAsync(int groupUserId)
    {
        var groupUser =await groupUserRepository.FindAsync(groupUserId)!;
        if (groupUser == null)
            throw new ApplicationException("GroupUser not found");
        groupUserRepository.Delete(groupUser);
        await groupUserRepository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<GroupUserDto>> GetGroupUserAsync(int groupUserId)
    {
        var groupUser = await groupUserRepository.AsQueryable().Include(x => x.Group).Include(z => z.Emp.Citizen).FirstOrDefaultAsync(x => x.Id == groupUserId);
        var groupUserDto = mapper.Map<GroupUserDto>(groupUser);
        return GResponse<GroupUserDto>.CreateSuccess(groupUserDto);
    }

    public async Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersAsync()
    {
        var groupUsers =await groupUserRepository.AsQueryable().Include(x => x.Group).Include(z => z.Emp.Citizen).AsSplitQuery().ToListAsync();
        var groupUserDtos = mapper.Map<IEnumerable<GroupUserDto>>(groupUsers);
        return GResponse<IEnumerable<GroupUserDto>>.CreateSuccess(groupUserDtos);
    }

    public async Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersByGroupIdAsync(int groupId)
    {
        var groupUsers =await groupUserRepository.AsQueryable().Include(x => x.Group).Include(z => z.Emp.Citizen).AsSplitQuery().Where(x => x.GroupId == groupId).ToListAsync();
        var groupUserDtos = mapper.Map<IEnumerable<GroupUserDto>>(groupUsers);
        return GResponse<IEnumerable<GroupUserDto>>.CreateSuccess(groupUserDtos);
    }

    public async Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersByUserIdAsync(int EmpId)
    {
        var groupUsers =await groupUserRepository.AsQueryable().Include(x => x.Group).Include(z => z.Emp.Citizen).AsSplitQuery().Where(x => x.EmpId == EmpId).ToListAsync();
        var groupUserDtos = mapper.Map<IEnumerable<GroupUserDto>>(groupUsers);
        return GResponse<IEnumerable<GroupUserDto>>.CreateSuccess(groupUserDtos);

    }

    public async Task<GResponse<GroupUserDto>> UpdateGroupUserAsync(GroupUserUpdateDto groupUserUpdateDto)
    {
        var groupUser =await groupUserRepository.Where(x=>x.Id== groupUserUpdateDto.Id)!.Include(x => x.Group).Include(z => z.Emp.Citizen).AsSplitQuery().FirstOrDefaultAsync();
        if (groupUser == null)
            throw new ApplicationException("GroupUser not found");
        groupUser = mapper.Map(groupUserUpdateDto, groupUser);
        await groupUserRepository.Commit();
        return GResponse<GroupUserDto>.CreateSuccess(mapper.Map<GroupUserDto>(groupUser));
    }
}
