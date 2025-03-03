using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IScheduleEntryRepository
    {
        Task AddScheduleEntry(ScheduleEntry scheduleEntry);
        Task<IEnumerable<ScheduleEntry>> GetAllScheduleEntries();
        Task<ScheduleEntry?> GetScheduleEntry(int id);
        Task UpdateScheduleEntry(ScheduleEntry scheduleEntry);
        Task DeleteScheduleEntry(int id);
    }
}
