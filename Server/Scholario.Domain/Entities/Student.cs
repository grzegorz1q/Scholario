using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Student : Person
    {
        public virtual Group Group { get; set; } = default!;
        public int GroupId { get; set; }
        public virtual Parent Parent { get; set; } = default!;
        public int ParentId { get; set; }
        public virtual ICollection<Grade> Grades { get; set; } = default!;
    }
}
