using Scholario.Application.Dtos.Group;

namespace Scholario.Application.Interfaces
{
    public interface IGroupService
    {
        Task<UserGroupsDto> GetLoggedUserGroup(int userId);
        Task<IEnumerable<ReadGroupDto>> GetLoggedTeacherGroups(int teacherId);
        Task<IEnumerable<ReadGroupDto>> GetAllGroups();
    }
}
