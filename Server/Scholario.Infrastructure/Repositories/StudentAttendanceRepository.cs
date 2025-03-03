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
    public class StudentAttendanceRepository : IStudentAttendanceRepository
    {
        private readonly AppDbContext _appDbContext;
        public StudentAttendanceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddStudentAttendance(StudentAttendance studentAttendance)
        {
            if (studentAttendance == null)
                throw new ArgumentNullException(nameof(studentAttendance));
            await _appDbContext.StudentAttendances.AddAsync(studentAttendance);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudentAttendance(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var studentAttendance = await _appDbContext.StudentAttendances.FirstOrDefaultAsync(s => s.Id == id);
            if (studentAttendance == null)
            {
                throw new KeyNotFoundException($"Obecność o ID {id} nie została znaleziona.");
            }
            _appDbContext.StudentAttendances.Remove(studentAttendance);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentAttendance>> GetAllStudentAttendances()
        {
            return await _appDbContext.StudentAttendances.ToListAsync();
        }

        public async Task<StudentAttendance?> GetStudentAttendance(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.StudentAttendances.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateStudentAttendance(StudentAttendance studentAttendance)
        {
            if (studentAttendance == null)
                throw new ArgumentNullException(nameof(studentAttendance));
            _appDbContext.StudentAttendances.Update(studentAttendance);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<StudentAttendance?> GetStudentAttendanceByStudentAndSubject(int studentId, int scheduleEntryId)
        {
            if (studentId < 0 || scheduleEntryId < 0)
                throw new ArgumentOutOfRangeException($"Podano błędne wartości argumentów: {studentId} {scheduleEntryId}");

            return await _appDbContext.StudentAttendances.FirstOrDefaultAsync(s => s.StudentId == studentId && s.ScheduleEntryId == scheduleEntryId);
        }
    }
}
