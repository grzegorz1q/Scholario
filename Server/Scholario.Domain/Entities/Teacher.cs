using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Teacher : Person
    {
       public virtual ICollection<Subject> Subjects { get; set; } = default!;
       public int? GroupId { get; set; }
    }
}
