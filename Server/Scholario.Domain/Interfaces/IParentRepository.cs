using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces.Repositories
{
    public interface IParentRepository
    {
        Task AddParent(Parent parent);
        Task<IEnumerable<Parent>> GetAllParents();
        Task<Parent?> GetParent(int id);
        Task UpdateParent(Parent parent);
        Task DeleteParent(int id);
    }
}
