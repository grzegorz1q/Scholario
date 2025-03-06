using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public float? GradeValue { get; set; }
        public GradeWeight GradeWeight { get; set; }
        public virtual Subject Subject { get; set; } = default!;
        public int SubjectId { get; set; }
        public virtual Student Student { get; set; } = default!;
        public int StudentId { get; set; }
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public virtual DescriptiveAssessment DescriptiveAssessment { get; set; } = default!;
        public int? DescriptiveAssessmentId { get; set; }
    }
}
