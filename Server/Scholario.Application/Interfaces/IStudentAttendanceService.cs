using Scholario.Application.Dtos.StudentAttendance;
using Scholario.Application.Dtos.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IStudentAttendanceService
    {
        Task CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto);
    }
}
