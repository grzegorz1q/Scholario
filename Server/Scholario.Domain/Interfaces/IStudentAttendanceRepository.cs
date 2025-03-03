using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IStudentAttendanceRepository
    {
        Task AddStudentAttendance(StudentAttendance studentAttendance);
        Task<IEnumerable<StudentAttendance>> GetAllStudentAttendances();
        Task<StudentAttendance?> GetStudentAttendance(int id);
        Task UpdateStudentAttendance(StudentAttendance studentAttendance);
        Task DeleteStudentAttendance(int id);
        Task<StudentAttendance?> GetStudentAttendanceByStudentAndSubject(int studentId, int scheduleEntryId);
    }
}
