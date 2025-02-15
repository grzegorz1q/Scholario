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
        private readonly IMapper _mapper;

        public ScheduleEntryService(ISubjectRepository subjectRepository, IGroupRepository groupRepository,
            ILessonHourRepository lessonHourRepository, IScheduleEntryRepository scheduleEntryRepository, 
            IStudentRepository studentRepository,ITeacherRepository teacherRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _groupRepository = groupRepository;
            _scheduleEntryRepository = scheduleEntryRepository;
            _studentRepository = studentRepository;
            _lessonHourRepository = lessonHourRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<LessonHour> CreateLessonHour(LessonHourDto lessonHourDto)
        {
            if(lessonHourDto == null)
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

        public async Task<StudentScheduleDto> GetStudentSchedule(int studentId)
        {
            if(studentId < 0)
                throw new ArgumentOutOfRangeException(nameof(studentId));

            var student = await _studentRepository.GetStudent(studentId);
            if (student == null)
                throw new Exception("Student not found");

            var group = student.Group;
            if (group == null)
                throw new Exception("Group not found");

            var scheduleEntries = group.ScheduleEntries
            .Select(se => new ScheduleEntryDto
            {
                SubjectId=se.SubjectId,
                GroupId=se.GroupId,
                Day = se.Day,
                LessonHourId=se.LessonHourId,
            }).ToList();

            foreach (var entry in scheduleEntries)
            {
                var subject = await _subjectRepository.GetSubject(entry.SubjectId);
                if (subject == null)
                {
                    throw new Exception("Subject not found");
                }
                else
                {
                    var teacher = await _teacherRepository.GetTeacher(subject.TeacherId);
                    var teacherName = teacher != null
                        ? $"{teacher.FirstName} {teacher.LastName}"
                        : "Brak nauczyciela";

                    entry.TeacherName = teacherName;
                }
            }

            var scheduleWithTeacher = new StudentScheduleDto
            {
                StudentId = student.Id,
                ScheduleEntries = scheduleEntries
            };

            return scheduleWithTeacher;
        }
    }
}
