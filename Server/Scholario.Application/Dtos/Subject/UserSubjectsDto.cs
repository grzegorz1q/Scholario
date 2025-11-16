using Scholario.Application.Dtos.Parent;

namespace Scholario.Application.Dtos.Subject
{
    public class UserSubjectsDto
    {
        public IEnumerable<ReadSubjectDto>? Subjects { get; set; }
        public IEnumerable<ParentSubjectDto>? ParentSubjects { get; set; }
    }

}
