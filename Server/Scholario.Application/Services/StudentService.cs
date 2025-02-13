using AutoMapper;
using Scholario.Application.Dtos.Student;
using Scholario.Application.Interfaces;
using Scholario.Domain.Interfaces;
using System;
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
        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
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
    }
}
