namespace Scholario.Domain.Entities
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public virtual Subject Subject { get; set; } = default!;
        public int SubjectId { get; set; }
        public virtual Group Group { get; set; } = default!;
        public int GroupId { get; set; }
        public DayOfWeek Day { get; set; }
        public virtual LessonHour LessonHour { get; set; } = default!;
        public int LessonHourId { get; set; }
        public virtual Classroom Classroom { get; set; } = default!;
        public int ClassroomId { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; } = default!;
    }
}
