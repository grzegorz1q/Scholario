using Microsoft.EntityFrameworkCore;
using Scholario.Application.Interfaces.Repositories;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class ScheduleEntryRepository : IScheduleEntryRepository
    {
        private readonly AppDbContext _appDbContext;
        public ScheduleEntryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddScheduleEntry(ScheduleEntry scheduleEntry)
        {
            if (scheduleEntry == null)
                throw new ArgumentNullException(nameof(scheduleEntry));
            await _appDbContext.ScheduleEntries.AddAsync(scheduleEntry);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteScheduleEntry(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var scheduleEntry = await _appDbContext.ScheduleEntries.FirstOrDefaultAsync(se => se.Id == id);
            if (scheduleEntry == null)
            {
                throw new KeyNotFoundException($"Wpis do planu zajęć o ID {id} nie został znaleziony.");
            }
            _appDbContext.ScheduleEntries.Remove(scheduleEntry);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetAllScheduleEntries()
        {
            return await _appDbContext.ScheduleEntries.ToListAsync();
        }

        public async Task<ScheduleEntry?> GetScheduleEntry(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.ScheduleEntries.FirstOrDefaultAsync(se => se.Id == id);
        }

        public async Task UpdateScheduleEntry(ScheduleEntry scheduleEntry)
        {
            if (scheduleEntry == null)
                throw new ArgumentNullException(nameof(scheduleEntry));
            _appDbContext.ScheduleEntries.Update(scheduleEntry);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
