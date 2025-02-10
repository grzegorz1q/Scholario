using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface IParentRepository
    {
        Task AddParent(Parent parent);
        Task<IEnumerable<Parent>> GetAllParents();
        Task GetParent(int id);
        Task UpdateParent(Parent parent);
        Task DeleteParent(int id);
    }
}
