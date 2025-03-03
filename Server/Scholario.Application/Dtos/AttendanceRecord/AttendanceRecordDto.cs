using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.AttendanceRecord
{
    public class AttendanceRecordDto
    {
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
