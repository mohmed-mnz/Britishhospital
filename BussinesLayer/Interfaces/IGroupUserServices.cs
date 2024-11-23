using ApiContracts;
using ApiContracts.groupusers;

namespace BussinesLayer.Interfaces;

public interface IGroupUserServices
{
    Task<GResponse<GroupUserDto>> GetGroupUserAsync(int groupUserId);
    Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersAsync();
    Task<GResponse<GroupUserDto>> AddGroupUserAsync(GroupUserAddDto groupUserAddDto);
    Task<GResponse<GroupUserDto>> UpdateGroupUserAsync(GroupUserUpdateDto groupUserUpdateDto);
    Task<GResponse<bool>> DeleteGroupUserAsync(int groupUserId);
    Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersByGroupIdAsync(int groupId);
    Task<GResponse<IEnumerable<GroupUserDto>>> GetGroupUsersByUserIdAsync(int EmpId);

}
