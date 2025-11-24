using Scholario.Domain.Entities;

namespace Scholario.Application.Dtos.Grade
{
    public class AddOrUpdateGradeToStudentDto
    {
        public int? Id { get; set; }
        public float GradeValue { get; set; }
        public GradeWeight GradeWeight { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
    }
}
