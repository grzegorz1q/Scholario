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

                //if (group.Subjects == null || !group.Subjects.Any(s => s.Id == subject.Id))
                //    throw new Exception("Group is not asigned to subject");

                var lessonHour = await _lessonHourRepository.GetLessonByNumber(scheduleEntryDto.LessonNumber);
                if (lessonHour == null)
                    throw new Exception("LessonHour not found for the given LessonNumber");

                var scheduleEntry = _mapper.Map<ScheduleEntry>(scheduleEntryDto);
                scheduleEntry.LessonHourId = lessonHour.Id;  

                await _scheduleEntryRepository.AddScheduleEntry(scheduleEntry);

                return scheduleEntry;
            }

        public async Task<IEnumerable<ReadScheduleEntryDto>> GetUserSchedule(int userId)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            var person = await _personRepository.GetPerson(userId);
            if (person == null)
                throw new KeyNotFoundException("User not found");

            if (person is Student student)
            {
                var group = student.Group;
                if (group == null)
                    throw new KeyNotFoundException("Group not found");

                return _mapper.Map<IEnumerable<ReadScheduleEntryDto>>(group.ScheduleEntries);

            }
            else if (person is Teacher teacher)
            {
                var subjects = teacher.Subjects;
                if (subjects == null || subjects.Count == 0)
                    throw new Exception("Teacher has no assigned subjects");
                var teacherScheduleEntries = subjects.SelectMany(s => s.ScheduleEntries);
                return teacherScheduleEntries.Select(e => _mapper.Map<ReadScheduleEntryDto>(e));
            }
            else if (person is Parent parent)
            {
                var parentScheduleEntries = new List<ReadScheduleEntryDto>();
                var students = parent.Students;
                if (students == null || students.Count == 0)
                    throw new Exception("This parent doesn't have any students");

                foreach (var stu in students)
                {
                    var group = stu.Group;
                    if (group == null)
                        throw new Exception($"Student {stu.FirstName} {stu.LastName} has no group assigned");

                    foreach(var entry in group.ScheduleEntries)
                    {
                        var dto = _mapper.Map<ReadScheduleEntryDto>(entry);
                        dto.StudentId = stu.Id;
                        dto.StudentName = $"{stu.FirstName} {stu.LastName}";
                        parentScheduleEntries.Add(dto);
                    }
                }
                return parentScheduleEntries;
            }
            else
            {
                throw new Exception("Invalid user type");
            }
        }
    }
}
