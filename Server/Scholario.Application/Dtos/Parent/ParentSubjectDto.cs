using Scholario.Application.Dtos.Subject;

namespace Scholario.Application.Dtos.Parent
{
    public class ParentSubjectDto
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; } = string.Empty;
        public ICollection<ReadSubjectDto> Subjects { get; set; } = default!;
    }
}
