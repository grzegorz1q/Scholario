namespace Scholario.Application.Dtos.ScheduleEntries
{
    public class StudentScheduleDto
    {
        public virtual ICollection<ScheduleEntryDto> ScheduleEntries { get; set; } = default!;
    }
}
