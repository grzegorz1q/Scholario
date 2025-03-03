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
        private readonly IMapper _mapper;
        public StudentAttendanceService(IMapper mapper, IStudentAttendanceRepository studentAttendanceRepository)
        {
            _studentAttendanceRepository = studentAttendanceRepository;
            _mapper = mapper;
        }
        public async Task CreateStudentAttendance(CreateStudentAttendanceDto studentAttendanceDto)
        {
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
