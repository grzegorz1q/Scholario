using Scholario.Application.Dtos.Subject;

namespace Scholario.Application.Interfaces
{
    public interface ISubjectService
    {
        Task CreateSubject(CreateSubjectDto createSubjectDto);
        Task<UserSubjectsDto> GetLoggedUserSubjects(int userId);
        Task<ReadSubjectDto> GetSubjectById(int subjectId);
        Task<IEnumerable<ReadSubjectDto>> GetSubjects();
    }
}
