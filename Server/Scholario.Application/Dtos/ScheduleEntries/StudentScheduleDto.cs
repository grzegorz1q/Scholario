﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class StudentScheduleDto
    {
        public virtual ICollection<ScheduleEntryDto> ScheduleEntries { get; set; } = default!;
    }
}
