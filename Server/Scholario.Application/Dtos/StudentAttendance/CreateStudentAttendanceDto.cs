using Scholario.Application.Dtos.AttendanceRecord;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.StudentAttendance
{
    public class CreateStudentAttendanceDto
    {
        public int StudentId { get; set; }
        public int ScheduleEntryId { get; set; }
        public ICollection<AttendanceRecordDto> AttendanceRecords { get; set; } = default!;
    }
}
