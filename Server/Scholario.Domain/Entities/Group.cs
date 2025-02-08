using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual Teacher Teacher { get; set; } = default!;
        public int TeacherId { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; } = default!;
        public virtual ICollection<Student> Students { get; set; } = default!;
    }
}
