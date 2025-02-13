using AutoMapper;
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
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        public async Task CreateSubject(CreateSubjectDto createSubjectDto)
        {
            if(createSubjectDto == null)
                throw new ArgumentNullException(nameof(createSubjectDto));
            
            var newSubject = _mapper.Map<Subject>(createSubjectDto);
            await _subjectRepository.AddSubject(newSubject);
        }
    }
}
