using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface ILessonHourRepository
    {
        Task AddLessonHour(LessonHour lessonHour);
        Task<IEnumerable<LessonHour>> GetAllLessonHours();
        Task<LessonHour?> GetLessonHour(int id);
        Task<LessonHour?> GetLessonByNumber(int lessonNumber);
        Task UpdateLessonHour(LessonHour lessonHour);
        Task DeleteLessonHour(int id);
    }
}
