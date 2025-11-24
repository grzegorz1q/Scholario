namespace Scholario.Application.Dtos.Grade
{
    public class SubjectGradesDto
    {
        public string SubjectName { get; set; } = default!;
        public IEnumerable<ReadGradeByStudentDto?> Grades { get; set; } = default!;
    }
}
