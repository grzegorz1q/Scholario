using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.Subject;
using Scholario.Application.Dtos.Teacher;

namespace Scholario.Application.Interfaces
{
    public interface ISubjectService
    {
        Task CreateSubject(CreateSubjectDto createSubjectDto);
        Task AddSubjectToGroup(AddSubjectToGroupDto addSubjectToGroup);
        Task<UserSubjectsDto> GetLoggedUserSubjects(int userId);
        Task<ReadSubjectDto> GetSubjectById(int subjectId);
        Task<IEnumerable<ReadSubjectDto>> GetSubjects();
        Task<IEnumerable<ReadGroupDto>> GetSubjectsByGroupId(int groupId);
    }
}
