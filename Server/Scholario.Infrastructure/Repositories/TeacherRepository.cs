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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _appDbContext;
        public TeacherRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddTeacher(Teacher teacher)
        {
            if(teacher == null) 
                throw new ArgumentNullException(nameof(teacher));
            await _appDbContext.Persons.AddAsync(teacher);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int id)
        {
            if (id < 0) 
                throw new ArgumentOutOfRangeException(nameof(id));
            var teacher = await _appDbContext.Persons.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
            if (teacher == null)
            {
                throw new KeyNotFoundException($"Nauczyciel o ID {id} nie został znaleziony.");
            }
            _appDbContext.Persons.Remove(teacher);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            return await _appDbContext.Persons.OfType<Teacher>().ToListAsync();
        }

        public async Task<Teacher?> GetTeacher(int id)
        {
            if(id < 0) 
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Persons.OfType<Teacher>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            if(teacher ==null)
                throw new ArgumentNullException(nameof(teacher));
            _appDbContext.Persons.Update(teacher);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
