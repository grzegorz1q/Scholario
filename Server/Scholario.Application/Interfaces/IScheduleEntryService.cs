using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Domain.Entities;

namespace Scholario.Application.Interfaces
{
    public interface IScheduleEntryService
    {
        Task<ScheduleEntry> CreateScheduleEntry(ScheduleEntryDto scheduleEntryDto);
        Task<IEnumerable<ReadScheduleEntryDto>> GetUserSchedule(int userId);
        Task<IEnumerable<ReadScheduleEntryDto>> GetScheduleByGroupId(int groupId);
       // Task<IEnumerable<ScheduleEntryDto>> SaveScheduleEntriesForGroup(int groupId, IEnumerable<ScheduleEntryDto> entries); // ewentualnie ScheduleEntryDto[] zamiast IEnumerable
    }
}
