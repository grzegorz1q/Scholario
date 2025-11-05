using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual Teacher Teacher { get; set; } = default!;
        public int TeacherId { get; set; }
        public virtual ICollection<Grade> Grades { get; set; } = default!;
        public virtual ICollection<Group> Groups { get; set; } = default!;
        public virtual ICollection<ScheduleEntry> ScheduleEntries { get; set; } = default!;
    }
}