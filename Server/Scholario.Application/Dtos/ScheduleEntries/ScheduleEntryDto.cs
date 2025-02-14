using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class ScheduleEntryDto
    {
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public DayOfWeek Day { get; set; }
        public int LessonHourId { get; set; }
    }
}
