using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IScheduleEntryService
    {
        Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto);
        Task<ScheduleEntry> CreateScheduleEntry(ScheduleEntryDto scheduleEntryDto);
        Task<StudentScheduleDto> GetStudentSchedule(int userId);
    }
}
