using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class LessonHourRepository : ILessonHourRepository
    {
        private readonly AppDbContext _appDbContext;
        public LessonHourRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddLessonHour(LessonHour lessonHour)
        {
            if (lessonHour == null)
                throw new ArgumentNullException(nameof(lessonHour));
            await _appDbContext.LessonHours.AddAsync(lessonHour);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteLessonHour(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var lessonHour = await _appDbContext.LessonHours.FirstOrDefaultAsync(l => l.Id == id);
            if (lessonHour == null)
            {
                throw new KeyNotFoundException($"Godzina lekcyjna o ID {id} nie został znaleziony.");
            }
            _appDbContext.LessonHours.Remove(lessonHour);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<LessonHour>> GetAllLessonHours()
        {
            return await _appDbContext.LessonHours.ToListAsync();
        }

        public async Task<LessonHour?> GetLessonHour(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.LessonHours.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<LessonHour?> GetLessonByNumber(int lessonNumber)
        {
            if (lessonNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(lessonNumber), "Lesson number must be a positive integer.");

            return await _appDbContext.LessonHours
                .FirstOrDefaultAsync(l => l.LessonNumber == lessonNumber);
        }

        public async Task UpdateLessonHour(LessonHour lessonHour)
        {
            if (lessonHour == null)
                throw new ArgumentNullException(nameof(lessonHour));
            _appDbContext.LessonHours.Update(lessonHour);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
