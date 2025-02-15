using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Group
{
    public class ReadGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TeacherName { get; set; } = default!;
    }
}
