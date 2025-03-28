﻿using Scholario.Domain.Entities;
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
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public int? DescriptiveAssessmentId { get; set; }
    }
}
