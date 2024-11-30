using ApiContracts;
using ApiContracts.Group;
using AutoMapper;
using BussinesLayer.Interfaces;
using DataLayer.Interfaces;
using Models.Models;

namespace BussinesLayer.Services;

public class GroupServices(IGroupRepository _repository, IMapper _mapper) : IGroupServices
{
    public async Task<GResponse<GroupDto>> AddGroupAsync(GroupAddDto groupAddDto)
    {
        var group = _mapper.Map<Group>(groupAddDto);
        await _repository.InsertAsync(group);
        await _repository.Commit();
        return GResponse<GroupDto>.CreateSuccess(_mapper.Map<GroupDto>(group));
    }


    public async Task<GResponse<bool>> DeleteGroupAsync(int groupId)
    {
        var group = await _repository.FindAsync(groupId)!;
        if (group == null)
            throw new ApplicationException("Group not found");
        _repository.Delete(group);
        await _repository.Commit();
        return GResponse<bool>.CreateSuccess(true);
    }

    public async Task<GResponse<GroupDto>> GetGroupAsync(int groupId)
    {
        var group = await _repository.FindAsync(groupId)!;
        if (group == null)
            throw new ApplicationException("Group not found");
        return GResponse<GroupDto>.CreateSuccess(_mapper.Map<GroupDto>(group));
    }

    public async Task<GResponse<IEnumerable<GroupDto>>> GetGroupsAsync()
    {
        var groups = await _repository.GetAllAsync();
        return GResponse<IEnumerable<GroupDto>>.CreateSuccess(_mapper.Map<IEnumerable<GroupDto>>(groups));
    }

    public async Task<GResponse<GroupDto>> UpdateGroupAsync(GroupUpdateDto groupUpdateDto)
    {
        var group = await _repository.FindAsync(groupUpdateDto.Id)!;
        if (group == null)
            throw new ApplicationException("Group not found");
        group = _mapper.Map(groupUpdateDto, group);
        await _repository.Commit();
        return GResponse<GroupDto>.CreateSuccess(_mapper.Map<GroupDto>(group));

    }
}
