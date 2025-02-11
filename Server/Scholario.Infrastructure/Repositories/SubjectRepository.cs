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
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _appDbContext;
        public SubjectRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));
            await _appDbContext.Subjects.AddAsync(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteSubject(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var subject = await _appDbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Przedmiot o ID {id} nie został znaleziony.");
            }
            _appDbContext.Subjects.Remove(subject);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            return await _appDbContext.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetSubject(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateSubject(Subject subject)
        {
            if (subject == null)
                throw new ArgumentNullException(nameof(subject));
            _appDbContext.Subjects.Update(subject);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
