namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class ReadScheduleEntryDto
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public DayOfWeek Day { get; set; }
        public int LessonNumber { get; set; }
    }
}
