namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class ScheduleEntryDto
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public DayOfWeek Day { get; set; }
        public int LessonNumber { get; set; }
        public int ClassroomNumber { get; set; }
    }
}
