using AutoMapper;
using Scholario.Application.Dtos.Grade;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Interfaces;
using Scholario.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository,IGradeRepository gradeRepository, IGroupRepository groupRepository, ISubjectRepository subjectRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
            _gradeRepository = gradeRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task AddOrChangeStudentGroup(AddOrChangeStudentToGroupDto addOrChangeStudentToGroupDto)
        {
            if (addOrChangeStudentToGroupDto == null)
                throw new ArgumentNullException(nameof(addOrChangeStudentToGroupDto));

            var student = await _studentRepository.GetStudent(addOrChangeStudentToGroupDto.StudentId);
            if (student == null)
                throw new Exception("Student not found");

            var group = await _groupRepository.GetGroup(addOrChangeStudentToGroupDto.GroupId);
            if (group == null)
                throw new Exception("Group not found");

            student.Group = group;
            await _studentRepository.UpdateStudent(student);

        }
        public async Task<ReadStudentDto?> GetStudentById(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("Id must be greater than 0");

            var student = await _studentRepository.GetStudent(id);

            if (student == null)
                throw new Exception("Student not found");

            var studentDto = _mapper.Map<ReadStudentDto>(student);

            return studentDto;
        }

        public async Task<IEnumerable<SubjectGradesDto>> GetStudentGrade(int studentId)
        {
            if (studentId < 0) 
                throw new ArgumentOutOfRangeException("Id must be greater than 0");

            var grades = await _gradeRepository.GetStudentGrades(studentId);

            if (grades == null)
                throw new Exception("Student dosnt have any grades");

            var subjectGrades = grades
                .GroupBy(g => g.SubjectId) // Grupujemy po SubjectId
                .Select(group => new SubjectGradesDto
                {
                    SubjectName = group.First().Subject.Name, // Używamy nazwy przedmiotu z pierwszej oceny w grupie
                    Grades = group.Select(g => g.GradeValue).ToList() // Lista ocen przypisanych do tego przedmiotu
                })
                .ToList();

            return subjectGrades;
        }

        public async Task<IEnumerable<ReadStudentWithFilteredGradesDto>> GetStudentsByGroupAndSubject(int groupId, int subjectId, int teacherId)
        {
            var subject = await _subjectRepository.GetSubject(subjectId);
            if (subject == null)
                throw new Exception("Subject not found");

            var group = subject.Groups.FirstOrDefault(g => g.Id == groupId);
            if(group == null)
                throw new Exception("The subject is not taught in this group.");

            if (subject.TeacherId != teacherId && group.TeacherId != teacherId)
                throw new UnauthorizedAccessException("You are not authorized to view this subject");

            var students = group.Students.ToList();
            var studentDtos = _mapper.Map<IEnumerable<ReadStudentWithFilteredGradesDto>>(students, opt => opt.Items["subjectId"] = subjectId);
            return studentDtos;
        }
    }
}
