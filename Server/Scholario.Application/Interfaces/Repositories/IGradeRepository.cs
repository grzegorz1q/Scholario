using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface IGradeRepository
    {
        Task AddGrade(Grade grade);
        Task<IEnumerable<Grade>> GetAllGrades();
        Task<Grade?> GetGrade(int id);
        Task UpdateGrade(Grade grade);
        Task DeleteGrade(int id);
    }
}
