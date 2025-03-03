using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; } = default!;
        public int StudentId { get; set; }
        public virtual Lesson Lesson { get; set; } = default!;
        public int LessonId { get; set; }
        public bool IsPresent { get; set; }
    }
}
