using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Domain.Entities;

namespace Scholario.Application.Interfaces
{
    public interface ILessonHourService
    {
        Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto);
    }
}
