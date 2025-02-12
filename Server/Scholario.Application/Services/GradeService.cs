using AutoMapper;
using Scholario.Application.Dtos;
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
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GradeService(IGradeRepository gradeRepository,IStudentRepository studentRepository,ISubjectRepository subjectRepository ,IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task AddGradeToStudent(AddOrUpdateGradeToStudentDto addGradeToStudentDto)
        {
            if(addGradeToStudentDto == null)
                throw new ArgumentNullException(nameof(addGradeToStudentDto));

            var student = await _studentRepository.GetStudent(addGradeToStudentDto.StudentId);
            if (student == null)
                throw new Exception("Student not found");

            var subject = await _subjectRepository.GetSubject(addGradeToStudentDto.SubjectId);
            if (subject == null)
                throw new Exception("Subject not found");

            var grade =  _mapper.Map<Grade>(addGradeToStudentDto);
            await _gradeRepository.AddGrade(grade);
        }
        public async Task UpdateStudentGrade(AddOrUpdateGradeToStudentDto updateStudentGradeDto)
        {
            if (updateStudentGradeDto == null)
                throw new ArgumentNullException(nameof(updateStudentGradeDto));

            var student = await _studentRepository.GetStudent(updateStudentGradeDto.StudentId);
            if (student == null)
                throw new Exception("Student not found");
            var subject = await _subjectRepository.GetSubject(updateStudentGradeDto.SubjectId);
            if (subject == null)
                throw new Exception("Subject not found");

            var grade = await _gradeRepository.GetGrade(updateStudentGradeDto.Id);
            if (grade == null)
            {
                throw new KeyNotFoundException(nameof(updateStudentGradeDto));
            }
            grade.GradeValue = updateStudentGradeDto.GradeValue;
            await _gradeRepository.UpdateGrade(grade);
        }
        public async Task DeleteGradeFromStudent(int id)
        {
            var grade = await _gradeRepository.GetGrade(id);
            if (grade == null)
                throw new KeyNotFoundException("Grade not found");

            await _gradeRepository.DeleteGrade(id);
        }
    }
}
