using Scholario.Application.Dtos.LessonHour;
using Scholario.Domain.Entities;

namespace Scholario.Application.Interfaces
{
    public interface ILessonHourService
    {
        Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto);
        Task<IEnumerable<LessonHourDto>> GetAllLessonHours();
    }
}
