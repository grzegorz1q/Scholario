using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Grade
{
    public class ReadGradeByStudentDto
    {
        public int Id { get; set; }
        public float? GradeValue { get; set; }
        public int GradeWeight { get; set; }
        public string GradeWeightName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public int? DescriptiveAssessmentId { get; set; }
    }
}
