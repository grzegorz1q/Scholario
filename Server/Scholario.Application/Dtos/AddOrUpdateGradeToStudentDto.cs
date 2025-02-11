using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos
{
    public class AddOrUpdateGradeToStudentDto
    {
        public int? Id { get; set; }
        public int GradeValue { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
    }
}
