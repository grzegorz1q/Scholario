namespace Scholario.Domain.Entities
{
    public class LessonHour
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int LessonNumber { get; set; }
        public virtual ICollection<ScheduleEntry> ScheduleEntries { get; set; } = default!;
    }
}
