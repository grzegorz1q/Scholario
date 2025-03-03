using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Teacher
{
    public class AddOrChangeTeacherToGroupDto
    {
        public int TeacherId { get; set; }
        public int GroupId { get; set; }
    }
}
