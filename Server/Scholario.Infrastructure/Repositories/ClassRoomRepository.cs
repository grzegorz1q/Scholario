using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly AppDbContext _appDbContext;
        public ClassroomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Classroom?> GetClassroomByNumber(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));
            return await _appDbContext.Classrooms.FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<bool> IsClassroomOccupied(int classroomId, DayOfWeek day, int lessonNumber)
        {
            var lessonHour = await _appDbContext.LessonHours.FirstOrDefaultAsync(lh => lh.LessonNumber == lessonNumber);

            if (lessonHour == null)
                return false;

            return await _appDbContext.ScheduleEntries.AnyAsync(e => e.ClassroomId == classroomId && e.Day == day && e.LessonHourId == lessonHour.Id);
        }

        public async Task<IEnumerable<Classroom>> GetClassrooms()
        {
            return await _appDbContext.Classrooms.ToListAsync();
        }
    }
}
