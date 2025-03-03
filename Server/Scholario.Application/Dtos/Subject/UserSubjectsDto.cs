using Scholario.Application.Dtos.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Subject
{
    public class UserSubjectsDto
    {
        public IEnumerable<ReadSubjectDto>? Subjects { get; set; }
        public IEnumerable<ParentSubjectDto>? ParentSubjects { get; set; }
    }

}
