namespace Scholario.Domain.Entities
{
    public class Classroom
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<ScheduleEntry> ScheduleEntries { get; set; } = default!;
    }
}
