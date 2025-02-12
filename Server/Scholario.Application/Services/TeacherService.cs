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
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository,IStudentRepository studentRepository,IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task AddMessageOrNoteToStudent(AddMessageOrNoteToStudentDto addMessageOrNoteToStudentDto)
        {
            if(addMessageOrNoteToStudentDto == null)
                throw new ArgumentNullException(nameof(addMessageOrNoteToStudentDto));

            var sender = await _teacherRepository.GetTeacher(addMessageOrNoteToStudentDto.SenderId);
            if(sender == null)
                throw new Exception("Teacher not found");

            var receiver = await _studentRepository.GetStudent(addMessageOrNoteToStudentDto.ReceiverId);
            if (receiver == null)
                throw new Exception("Student not found");

            var note = _mapper.Map<Message>(addMessageOrNoteToStudentDto);
            receiver.ReceivedMessages.Add(note);

            if(addMessageOrNoteToStudentDto.MessageType == MessageType.StudentNote)
            {
                var parent = receiver.Parent;
                if(parent != null)
                {
                    var parentNote = new AddMessageOrNoteToStudentDto
                    {
                        SenderId = sender.Id,
                        ReceiverId = parent.Id,
                        Content = addMessageOrNoteToStudentDto.Content,
                        MessageType = MessageType.StudentNote
                    };

                    var parentNoteMap = _mapper.Map<Message>(parentNote);
                    parent.ReceivedMessages.Add(parentNoteMap);
                }
            }
            await _studentRepository.UpdateStudent(receiver);
        }

        public async Task<ReadStudentDto?> GetStudentById(ReadStudentDto readStudentDto)
        {
            if(readStudentDto == null)
                throw new ArgumentNullException(nameof(readStudentDto));

            var student = await _studentRepository.GetStudent(readStudentDto.StudentId);

            if (student == null)
                throw new Exception("Student not found");

            var studentDto =  _mapper.Map<ReadStudentDto>(student);

            studentDto.ReadGradeByStudentDtos = student.Grades.Select(g => new ReadGradeByStudentDto
            {
                Id = g.Id,
                GradeValue = g.GradeValue,
                SubjectId = g.SubjectId,
                StudentId = g.StudentId,
                DateOfIssue = g.DateOfIssue,
                DescriptiveAssessmentId = g.DescriptiveAssessmentId
            }).ToList();

            return studentDto;
        }
    }
}
