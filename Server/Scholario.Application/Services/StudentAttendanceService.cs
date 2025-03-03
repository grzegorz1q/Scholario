using AutoMapper;
using Newtonsoft.Json;
using Scholario.Application.Dtos.AttendanceRecord;
using Scholario.Application.Dtos.StudentAttendance;
using Scholario.Application.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;
using System.Collections.Generic;

namespace Scholario.Application.Services
{
    public class StudentAttendanceService : IStudentAttendanceService
    {
        private readonly IStudentAttendanceRepository _studentAttendanceRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IScheduleEntryRepository _scheduleEntryRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public StudentAttendanceService(IMapper mapper,ITeacherRepository teacherRepository ,IStudentAttendanceRepository studentAttendanceRepository, IScheduleEntryRepository scheduleEntryRepository, IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            _studentAttendanceRepository = studentAttendanceRepository;
            _groupRepository = groupRepository;
            _scheduleEntryRepository = scheduleEntryRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        public async Task CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto, int teacherId)
        {
            if (studentAttendanceDto == null)
                throw new ArgumentNullException(nameof(studentAttendanceDto));

            var student = await _studentRepository.GetStudent(studentAttendanceDto.StudentId);
            if (student == null)
                throw new Exception("Student not found");

            var scheduleEntry = await _scheduleEntryRepository.GetScheduleEntry(studentAttendanceDto.ScheduleEntryId);
            if (scheduleEntry == null)
                throw new Exception("ScheduleEntry not found");

            var group = scheduleEntry.Group;

            if (!group.Students.Any(s => s.Id == student.Id))
                throw new Exception("This student is not in this group");

            var teacher = await _teacherRepository.GetTeacher(teacherId);
            if (teacher == null)
                throw new UnauthorizedAccessException(nameof(teacher));

            //Do zmiany

/*            if (!teacher.Subjects.Any(subject => subject.Groups.Any(g => g.Id == group.Id)))
                throw new Exception("This teacher is not teaching this group");*/
/*
            if (!scheduleEntry.Group.Subjects.Any(subject => subject.TeacherId == teacher.Id))
                throw new Exception("This teacher is not teaching this subject");*/

            var existingStudentAttendance = await _studentAttendanceRepository.GetStudentAttendanceByStudentAndSubject(studentAttendanceDto.StudentId, studentAttendanceDto.ScheduleEntryId);
            if (existingStudentAttendance == null)
            {
                var attendanceJson = JsonConvert.SerializeObject(studentAttendanceDto.AttendanceRecords);
                var studentAttendance = new StudentAttendance
                {
                    StudentId = studentAttendanceDto.StudentId,
                    ScheduleEntryId = studentAttendanceDto.ScheduleEntryId,
                    AttendanceJson = attendanceJson
                };

                // Dodanie rekordu obecności do repozytorium
                await _studentAttendanceRepository.AddStudentAttendance(studentAttendance);
            }
            else
            {
                // Deserializujemy istniejący JSON z obecnościami
                var existingAttendanceRecords = JsonConvert.DeserializeObject<List<AttendanceRecordDto>>(existingStudentAttendance.AttendanceJson);

                // Dodajemy nowe rekordy obecności
                existingAttendanceRecords.AddRange(studentAttendanceDto.AttendanceRecords);

                // Serializujemy zaktualizowaną listę obecności
                existingStudentAttendance.AttendanceJson = JsonConvert.SerializeObject(existingAttendanceRecords);

                // Aktualizujemy rekord w repozytorium
                await _studentAttendanceRepository.UpdateStudentAttendance(existingStudentAttendance);
            }
        }
    }
}
