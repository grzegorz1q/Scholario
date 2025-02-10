using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task AddGroup(Group group);
        Task<IEnumerable<Group>> GetAllGroups();
        Task GetGroup(int id);
        Task UpdateGroup(Group group);
        Task DeleteGroup(int id);
    }
}
