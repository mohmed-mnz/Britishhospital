using ApiContracts;
using ApiContracts.Group;

namespace BussinesLayer.Interfaces;

public interface IGroupServices
{
    Task<GResponse<GroupDto>> GetGroupAsync(int groupId);
    Task<GResponse<IEnumerable<GroupDto>>> GetGroupsAsync();
    Task<GResponse<GroupDto>> AddGroupAsync(GroupAddDto groupAddDto);
    Task<GResponse<GroupDto>> UpdateGroupAsync(GroupUpdateDto groupUpdateDto);
    Task<GResponse<bool>> DeleteGroupAsync(int groupId);

}
