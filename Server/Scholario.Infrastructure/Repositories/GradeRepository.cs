using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Interfaces.Repositories;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _appDbContext;
        public GradeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddGrade(Grade grade)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade));
            await _appDbContext.Grades.AddAsync(grade);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteGrade(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var grade = await _appDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
            if (grade == null)
            {
                throw new KeyNotFoundException($"Ocena o ID {id} nie została znaleziona.");
            }
            _appDbContext.Grades.Remove(grade);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Grade>> GetAllGrades()
        {
            return await _appDbContext.Grades.ToListAsync();
        }

        public async Task<Grade?> GetGrade(int? id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateGrade(Grade grade)
        {
            if (grade == null)
                throw new ArgumentNullException(nameof(grade));
            _appDbContext.Grades.Update(grade);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
