namespace Scholario.Application.Dtos.Grade
{
    public class ReadGradeByStudentDto
    {
        public int Id { get; set; }
        public float? GradeValue { get; set; }
        public int GradeWeight { get; set; }
        public string GradeWeightName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        public int? DescriptiveAssessmentId { get; set; }
    }
}
