using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; } = default!;
        public int StudentId { get; set; }
        public virtual ScheduleEntry ScheduleEntry { get; set; } = default!;
        public int ScheduleEntryId { get; set; }
        public string AttendanceJson { get; set; } = string.Empty;
    }
}
