using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces.Repositories
{
    public interface ILessonHourRepository
    {
        Task AddLessonHour(LessonHour lessonHour);
        Task<IEnumerable<LessonHour>> GetAllLessonHours();
        Task<LessonHour?> GetLessonHour(int id);
        Task UpdateLessonHour(LessonHour lessonHour);
        Task DeleteLessonHour(int id);
    }
}
