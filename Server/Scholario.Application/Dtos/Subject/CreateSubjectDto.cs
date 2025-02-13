using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Subject
{
    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int TeacherId { get; set; }
    }
}
