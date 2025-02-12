using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task AddStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student?> GetStudent(int id);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
    }
}
