using AutoMapper;
using Scholario.Application.Dtos.ScheduleEntries;
using Scholario.Application.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Services
{
    public class ScheduleEntryService : IScheduleEntryService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IScheduleEntryRepository _scheduleEntryRepository;
        private readonly ILessonHourRepository _lessonHourRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ScheduleEntryService(ISubjectRepository subjectRepository, IGroupRepository groupRepository,
            ILessonHourRepository lessonHourRepository, IScheduleEntryRepository scheduleEntryRepository,
            IStudentRepository studentRepository, ITeacherRepository teacherRepository, IPersonRepository personRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _groupRepository = groupRepository;
            _scheduleEntryRepository = scheduleEntryRepository;
            _studentRepository = studentRepository;
            _lessonHourRepository = lessonHourRepository;
            _teacherRepository = teacherRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto)
        {
            if (lessonHourDto == null)
            { throw new ArgumentNullException(nameof(lessonHourDto)); }

            var newlessonHour = new LessonHour
            {
                StartTime = lessonHourDto.StartTime,
                EndTime = lessonHourDto.EndTime,
                LessonNumber = lessonHourDto.LessonNumber
            };

            await _lessonHourRepository.AddLessonHour(newlessonHour);
            return newlessonHour;
        }


        public async Task<ScheduleEntry> CreateScheduleEntry(ScheduleEntryDto scheduleEntryDto)
        {
            if (scheduleEntryDto == null)
                throw new ArgumentNullException(nameof(scheduleEntryDto));

            var subject = await _subjectRepository.GetSubject(scheduleEntryDto.SubjectId);
            if (subject == null)
                throw new Exception("Subject not found");

            var group = await _groupRepository.GetGroup(scheduleEntryDto.GroupId);
            if (group == null)
                throw new Exception("Group not found");

            var scheduleEntry = new ScheduleEntry
            {
                SubjectId = scheduleEntryDto.SubjectId,
                GroupId = scheduleEntryDto.GroupId,
                Day = scheduleEntryDto.Day,
                LessonHourId = scheduleEntryDto.LessonHourId
            };

            await _scheduleEntryRepository.AddScheduleEntry(scheduleEntry);

            return scheduleEntry;
        }

        public async Task<StudentScheduleDto> GetStudentSchedule(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            var person = await _personRepository.GetPerson(userId);
            if (person == null)
                throw new Exception("User not found");

            var scheduleList = new StudentScheduleDto();
            var allScheduleEntries = new List<ScheduleEntry>();

            if (person is Student student)
            {
                var group = student.Group;
                if (group == null)
                    throw new Exception("Group not found");

                scheduleList.ScheduleEntries = _mapper.Map<ICollection<ScheduleEntryDto>>(group.ScheduleEntries);

            }
            else if (person is Teacher teacher)
            {
                var subjects = teacher.Subjects;
                if (subjects == null || !subjects.Any())
                    throw new Exception("Teacher has no assigned subjects");

                scheduleList.ScheduleEntries = _mapper.Map<ICollection<ScheduleEntryDto>>(subjects.SelectMany(s => s.ScheduleEntries));
            }
            else if (person is Parent parent)
            {
                var students = parent.Students;
                if (students == null || !students.Any())
                    throw new Exception("This parent doesn't have any students");


                foreach (var stu in students)
                {
                    var group = stu.Group;
                    if (group == null)
                        throw new Exception($"Student {stu.FirstName} {stu.LastName} has no group assigned");

                    allScheduleEntries.AddRange(group.ScheduleEntries);
                }

                scheduleList.ScheduleEntries = _mapper.Map<ICollection<ScheduleEntryDto>>(allScheduleEntries);
            }
            else
            {
                throw new Exception("Invalid user type");
            }

            foreach (var entry in scheduleList.ScheduleEntries)
            {
                var subject = await _subjectRepository.GetSubject(entry.SubjectId);
                if (subject == null)
                {
                    throw new Exception("Subject not found");
                }
                else
                {
                    var teacher = await _teacherRepository.GetTeacher(subject.TeacherId);
                    entry.TeacherName = teacher != null
                        ? $"{teacher.FirstName} {teacher.LastName}"
                        : "Brak nauczyciela";
                }
            }

            return scheduleList;
        }
    }
}
