using AutoMapper;
using Scholario.Application.Dtos.Grade;
using Scholario.Application.Dtos.Message;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Dtos.Teacher;
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
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public TeacherService(ITeacherRepository teacherRepository,IStudentRepository studentRepository,IGroupRepository groupRepository,IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
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

        public async Task AddOrChangeTeacherToGroup(AddOrChangeTeacherToGroupDto addTeacherToGroupDto)
        {
            if(addTeacherToGroupDto == null)
                throw new ArgumentNullException(nameof(addTeacherToGroupDto));

            var teacher = await _teacherRepository.GetTeacher(addTeacherToGroupDto.TeacherId);
            if (teacher == null)
                throw new Exception("Teacher not found");

            var group = await _groupRepository.GetGroup(addTeacherToGroupDto.GroupId);
            if (group == null)
                throw new Exception("Group not found");

            if (group.Teacher != null)
                throw new Exception("This group is already assigned to teacher");

            teacher.Group = group;
            await _teacherRepository.UpdateTeacher(teacher);
        }
    }
}
