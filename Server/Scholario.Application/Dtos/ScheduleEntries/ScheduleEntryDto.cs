using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class ScheduleEntryDto
    {
        public string TeacherName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public DayOfWeek Day { get; set; }
        public int LessonNumber { get; set; }
    }
}
