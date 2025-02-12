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
    public class AccountService : IAccountService
    {
        private readonly IParentRepository _parentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public AccountService(IParentRepository parentRepository, ITeacherRepository teacherRepository, IStudentRepository studentRepository, IPersonRepository personRepository, IMapper mapper) {
            _parentRepository = parentRepository;
            _teacherRepository = teacherRepository;
            _studentRepository = studentRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                throw new ArgumentNullException(nameof(registerUserDto));
            }
            var newUser = _mapper.Map<Person>(registerUserDto);
            await _personRepository.AddPerson(newUser);
        }
    }
}
