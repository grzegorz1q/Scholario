using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Teacher;

namespace Scholario.Application.Interfaces
{
    public interface ITeacherService
    {
        Task AddMessageOrNoteToStudent(AddMessageOrNoteToStudentDto addNoteToStudentDto);
        Task AddOrChangeTeacherToGroup(AddOrChangeTeacherToGroupDto addOrChangeTeacherToGroupDto);
    }
}
