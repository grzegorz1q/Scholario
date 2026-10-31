using Scholario.Application.Dtos.Grade;

namespace Scholario.Application.Interfaces
{
    public interface IGradeService
    {
        Task AddGradeToStudent(AddOrUpdateGradeToStudentDto addGradeToStudentDto, int teacherId);
        Task UpdateStudentGrade(AddOrUpdateGradeToStudentDto updateStudentGradeDto, int teacherId);
        Task DeleteGradeFromStudent(int id);
    }
}
