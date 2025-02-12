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

            var receiver = await _studentRepository.GetStudent(addMessageOrNoteToStudentDto.ReceverId);
            if (receiver == null)
                throw new Exception("Student not found");

            var note = _mapper.Map<Message>(addMessageOrNoteToStudentDto);
            receiver.ReceivedMessages.Add(note);
            await _studentRepository.UpdateStudent(receiver);

            var parent = receiver.Parent;
            parent.ReceivedMessages.Add(note); //to sie nie wysyla. Dostaje tylko uczeń. W tabeli messages powinny dodawac sie chyba dwa wpisy a dodaje sie tylko dla ucznia
        }

        public Task<IEnumerable<ReadStudentDto>> GetAllStudents(ReadStudentDto readStudentDto)
        {
            throw new NotImplementedException();
        }

        public Task<ReadStudentDto?> GetStudentById(ReadStudentDto readStudentDto)
        {
            throw new NotImplementedException();
        }
    }
}
