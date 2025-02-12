using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos
{
    public class ReadStudentDto
    {
        public int StudentId { get; set; }
        public virtual ICollection<ReadGradeByStudentDto> ReadGradeByStudentDtos { get; set; } = default!;
    }
}
