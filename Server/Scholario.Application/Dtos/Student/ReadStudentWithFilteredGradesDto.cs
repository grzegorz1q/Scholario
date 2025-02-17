using Scholario.Application.Dtos.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Student
{
    public class ReadStudentWithFilteredGradesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<ReadGradeByStudentDto> Grades { get; set; } = default!;
    }
}
