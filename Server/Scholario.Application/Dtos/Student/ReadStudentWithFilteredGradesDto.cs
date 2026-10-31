using Scholario.Application.Dtos.Grade;

namespace Scholario.Application.Dtos.Student
{
    public class ReadStudentWithFilteredGradesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<ReadGradeByStudentDto> Grades { get; set; } = default!;
    }
}
