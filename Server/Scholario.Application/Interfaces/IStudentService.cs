using Scholario.Application.Dtos.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ReadStudentDto?> GetStudentById(int id);
        Task AddOrChangeStudentGroup(AddOrChangeStudentToGroupDto addOrChangeStudentToGroupDto);
        Task<IEnumerable<ReadStudentDto>> GetStudentsByGroupAndSubject(int groupId, int subjectId, int teacherId);
    }
}
