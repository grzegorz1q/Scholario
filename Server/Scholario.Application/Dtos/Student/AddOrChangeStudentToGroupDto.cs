using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Student
{
    public class AddOrChangeStudentToGroupDto
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
    }
}
