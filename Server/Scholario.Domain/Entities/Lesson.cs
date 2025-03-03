using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public DateTime DateOfLesson { get; set; }
        public ScheduleEntry ScheduleEntry { get; set; } = default!;
        public int ScheduleEntryId { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = default!;
    }
}
