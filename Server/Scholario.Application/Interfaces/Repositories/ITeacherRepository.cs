using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task AddTeacher(Teacher teacher);
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task GetTeacher(int id);
        Task UpdateTeacher(Teacher teacher);
        Task DeleteTeacher(int id);
    }
}
