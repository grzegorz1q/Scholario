namespace Scholario.Domain.Entities
{
    public class Teacher : Person
    {
       public virtual ICollection<Subject> Subjects { get; set; } = default!;
       public virtual Group Group { get; set; } = default!;
    }
}
