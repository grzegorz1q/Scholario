using Scholario.Application.Dtos.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IGroupService
    {
        Task<UserGroupsDto> GetLoggedUserGroup(int userId);
        //Task<IEnumerable<ReadGroupDto>> GetLoggedTeacherGroups(int teacherId);
    }
}
