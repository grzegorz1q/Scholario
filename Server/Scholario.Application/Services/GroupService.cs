using AutoMapper;
using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.Parent;
using Scholario.Application.Dtos.Subject;
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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;
        public GroupService(IGroupRepository groupRepository, IPersonRepository personRepository, ITeacherRepository teacherRepository, IMapper mapper) 
        {
            _groupRepository = groupRepository;
            _personRepository = personRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadGroupDto>> GetLoggedTeacherGroups(int teacherId)
        {
            var teacher = await _teacherRepository.GetTeacher(teacherId);
            if (teacher == null)
                throw new UnauthorizedAccessException(nameof(teacher));
            var teacherGroups = teacher.Subjects.SelectMany(s => s.Groups);
            var groupDtos = _mapper.Map<IEnumerable<ReadGroupDto>>(teacherGroups);
            return groupDtos;
        }

        public async Task<UserGroupsDto> GetLoggedUserGroup(int userId)
        {
            var groups = await _groupRepository.GetAllGroups();
            if(!groups.Any())
                throw new ArgumentNullException(nameof(groups));
            var response = new UserGroupsDto();
            var person = await _personRepository.GetPerson(userId);
            if (person is Teacher)
            {
                var group = groups.FirstOrDefault(g => g.TeacherId == userId);
                response.Group = _mapper.Map<ReadGroupDto>(group);
            }
            else if (person is Student)
            {
                var group = groups.FirstOrDefault(g => g.Students.Any(s => s.Id == userId));
                response.Group = _mapper.Map<ReadGroupDto>(group);
            }
            else if (person is Parent parent)
            {
                var children = parent.Students.ToList();
                response.ParentGroups = _mapper.Map<IEnumerable<ParentGroupDto>>(children);
            }
            if (!groups.Any())
                throw new Exception("This user has no groups asigned");
            return response;
        }
    }
}
