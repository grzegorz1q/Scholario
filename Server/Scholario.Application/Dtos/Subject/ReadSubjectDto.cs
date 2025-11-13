using Scholario.Application.Dtos.Group;
namespace Scholario.Application.Dtos.Subject
{
    public class ReadSubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string TeacherName { get; set; } = default!;
        public virtual ICollection<ReadGroupDto> Groups { get; set; } = default!;
    }
}
