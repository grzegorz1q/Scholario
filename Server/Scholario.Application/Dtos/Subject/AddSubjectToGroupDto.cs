using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Subject
{
    public class AddSubjectToGroupDto
    {
        public int SubjectId {  get; set; }
        public int GroupId { get; set; }
    }
}
