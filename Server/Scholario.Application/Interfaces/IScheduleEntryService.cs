using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Domain.Entities;

namespace Scholario.Application.Interfaces
{
    public interface IScheduleEntryService
    {
        Task<ScheduleEntry> CreateScheduleEntry(ScheduleEntryDto scheduleEntryDto);
        Task<IEnumerable<ReadScheduleEntryDto>> GetUserSchedule(int userId);
    }
}
