using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Scholario.Application.Dtos.Group;
using Scholario.Application.Dtos.Parent;
using Scholario.Application.Dtos.Subject;
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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository subjectRepository, IPersonRepository personRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _personRepository = personRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task AddSubjectToGroup(AddSubjectToGroupDto addSubjectToGroup)
        {
            if (addSubjectToGroup == null) throw new ArgumentNullException(nameof(addSubjectToGroup));

            var subject = await _subjectRepository.GetSubject(addSubjectToGroup.SubjectId);
                if (subject == null) throw new ArgumentNullException(nameof(subject),"Subject not found");

            var group = await _groupRepository.GetGroup(addSubjectToGroup.GroupId);
                if (group == null) throw new ArgumentNullException(nameof(group),"Group not found");

            subject.Groups.Add(group);
            await _subjectRepository.UpdateSubject(subject);
        }

        public async Task CreateSubject(CreateSubjectDto createSubjectDto)
        {
            if (createSubjectDto == null)
                throw new ArgumentNullException(nameof(createSubjectDto));

            var newSubject = _mapper.Map<Subject>(createSubjectDto);
            await _subjectRepository.AddSubject(newSubject);
        }

        public async Task<UserSubjectsDto> GetLoggedUserSubjects(int userId)
        {
            var subjects = await _subjectRepository.GetAllSubjects();
            if (!subjects.Any())
            {
                throw new ArgumentNullException(nameof(subjects));
            }
            var response = new UserSubjectsDto();
            var person = await _personRepository.GetPerson(userId);
            if (person is Teacher)
            {
                subjects = subjects.Where(s => s.TeacherId == userId);
                response.Subjects = _mapper.Map<IEnumerable<ReadSubjectDto>>(subjects);
            }
            else if (person is Student student)
            {
                subjects = subjects.Where(s => s.Groups.Any(g => g.Id == student.GroupId));
                response.Subjects = _mapper.Map<IEnumerable<ReadSubjectDto>>(subjects);
            }
            else if (person is Parent parent)
            {
                var children = parent.Students.ToList();
                response.ParentSubjects = _mapper.Map<IEnumerable<ParentSubjectDto>>(children);
            }
            if (!subjects.Any())
                throw new Exception("This user has no subjects asigned");
            return response;
        }
        public async Task<ReadSubjectDto> GetSubjectById(int subjectId)
        {
            var subject = await _subjectRepository.GetSubject(subjectId) ?? throw new KeyNotFoundException("Subject not found!");
            return _mapper.Map<ReadSubjectDto>(subject);
        }

        public async Task<IEnumerable<ReadSubjectDto>> GetSubjects()
        {
            var subjects = await _subjectRepository.GetAllSubjects() ?? throw new KeyNotFoundException("Subjects not found");
            return _mapper.Map<IEnumerable<ReadSubjectDto>>(subjects);
        }

        public async Task<IEnumerable<ReadGroupDto>> GetSubjectsByGroupId(int groupId)
        {
            if (groupId < 0) throw new ArgumentOutOfRangeException(nameof(groupId));

            var group = await _groupRepository.GetGroup(groupId) ?? throw new KeyNotFoundException(nameof(groupId));
            var subjects = group.Subjects;

            return _mapper.Map<IEnumerable<ReadGroupDto>>(subjects);
        }
    }
}
