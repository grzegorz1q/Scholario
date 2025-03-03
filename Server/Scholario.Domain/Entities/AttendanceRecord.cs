using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class AttendanceRecord
    {
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }
}
