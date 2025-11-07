namespace Scholario.Application.Dtos.LessonHour
{
    public class LessonHourDto
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int LessonNumber { get; set; }
    }
}
