using Scholario.Application.Dtos.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Parent
{
    public class ParentSubjectDto
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public ICollection<ReadSubjectDto> Subjects { get; set; } = default!;
    }
}
