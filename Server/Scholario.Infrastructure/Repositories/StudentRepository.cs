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
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _appDbContext;
        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            await _appDbContext.Persons.AddAsync(student);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var student = await _appDbContext.Persons.OfType<Student>().FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new KeyNotFoundException($"Uczeń o ID {id} nie został znaleziony.");
            }
            _appDbContext.Persons.Remove(student);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _appDbContext.Persons.OfType<Student>().ToListAsync();
        }

        public async Task<Student?> GetStudent(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Persons.OfType<Student>().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            _appDbContext.Persons.Update(student);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
