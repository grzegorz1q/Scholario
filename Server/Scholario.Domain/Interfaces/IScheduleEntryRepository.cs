using Scholario.Domain.Entities;

namespace Scholario.Domain.Interfaces
{
    public interface IScheduleEntryRepository
    {
        Task AddScheduleEntry(ScheduleEntry scheduleEntry);
        Task<IEnumerable<ScheduleEntry>> GetAllScheduleEntries();
        Task<ScheduleEntry?> GetScheduleEntry(int id);
        Task UpdateScheduleEntry(ScheduleEntry scheduleEntry);
        Task DeleteScheduleEntry(int id);
        Task<bool> Exists(int groupId, DayOfWeek day, int lessonNumber);
        Task<IEnumerable<ScheduleEntry>> GetFilteredScheduleEntries(int? groupId, int? teacherId, int? classroomId);
    }
}
