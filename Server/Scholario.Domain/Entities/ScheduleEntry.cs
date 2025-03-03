using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public virtual Subject Subject { get; set; } = default!;
        public int SubjectId { get; set; }
        public virtual Group Group { get; set; } = default!;
        public int GroupId { get; set; }
        public DayOfWeek Day { get; set; }
        public virtual LessonHour LessonHour { get; set; } = default!;
        public int LessonHourId { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; } = default!;
    }
}
