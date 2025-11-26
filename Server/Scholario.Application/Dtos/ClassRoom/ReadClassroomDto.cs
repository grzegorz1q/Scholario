using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.ClassRoom
{
    public class ReadClassRoomDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<ReadScheduleEntryDto> ScheduleEntries { get; set; }
    }
}
