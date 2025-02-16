using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Classroom
    {
        public int Id { get; set; }
        public string roomNumber { get; set; } = string.Empty;
        public ICollection<ScheduleEntry> scheduleEntries { get; set; } = default!;

    }
}
