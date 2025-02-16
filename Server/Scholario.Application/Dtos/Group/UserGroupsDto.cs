using Scholario.Application.Dtos.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Group
{
    public class UserGroupsDto
    {
        public ReadGroupDto? Group { get; set; } = default!;
        public IEnumerable<ParentGroupDto>? ParentGroups { get; set; } = default!;
    }
}
