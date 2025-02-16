using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Parent
{
    public class ParentGroupDto
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public ReadGroupDto Group { get; set; } = default!;
    }
}
