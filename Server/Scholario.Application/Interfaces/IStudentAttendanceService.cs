using Scholario.Application.Dtos.StudentAttendance;

namespace Scholario.Application.Interfaces
{
    public interface IStudentAttendanceService
    {
        Task CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto, int teacherId);
    }
}
