using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task AddGroup(Group group);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<Group?> GetGroup(int id);
        Task UpdateGroup(Group group);
        Task DeleteGroup(int id);
    }
}
