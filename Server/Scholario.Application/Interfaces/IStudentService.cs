using Scholario.Application.Dtos.Grade;
using Scholario.Application.Dtos.Student;

namespace Scholario.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ReadStudentDto?> GetStudentById(int id);
        Task AddOrChangeStudentGroup(AddOrChangeStudentToGroupDto addOrChangeStudentToGroupDto);
        Task<IEnumerable<ReadStudentWithFilteredGradesDto>> GetStudentsByGroupAndSubject(int groupId, int subjectId, int teacherId);
        Task<IEnumerable<SubjectGradesDto>> GetStudentGrade(int studentId);
    }
}
