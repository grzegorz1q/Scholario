using Scholario.Domain.Entities;

namespace Scholario.Domain.Interfaces
{
    public interface IGradeRepository
    {
        Task AddGrade(Grade grade);
        Task<IEnumerable<Grade>> GetAllGrades();
        Task<IEnumerable<Grade>> GetStudentGrades(int studentId);
        Task<Grade?> GetGrade(int? id);
        Task UpdateGrade(Grade grade);
        Task DeleteGrade(int id);
    }
}
