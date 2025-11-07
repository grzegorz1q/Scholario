using AutoMapper;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;

namespace Scholario.Application.Services
{
    public class LessonHourService : ILessonHourService
    {
        private readonly ILessonHourRepository _lessonHourRepository;
        private readonly IMapper _mapper;
        public LessonHourService(ILessonHourRepository lessonHourRepository, IMapper mapper)
        {
            _lessonHourRepository = lessonHourRepository;
            _mapper = mapper;
        }
        public async Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto)
        {
            if (lessonHourDto == null)
                throw new ArgumentNullException(nameof(lessonHourDto));

            var newLessonHour = _mapper.Map<LessonHour>(lessonHourDto);
            await _lessonHourRepository.AddLessonHour(newLessonHour);

            return newLessonHour;
        }
    }
}
