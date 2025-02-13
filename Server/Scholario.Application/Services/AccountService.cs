using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scholario.Application.Dtos;
using Scholario.Application.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Scholario.Application.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace Scholario.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IPasswordHasher<Person> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IMapper _mapper;

        public AccountService(IPersonRepository personRepository, IPasswordHasher<Person> passwordHasher, AuthenticationSettings authenticationSettings, IMapper mapper) {
            _personRepository = personRepository;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _mapper = mapper;
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
            {
                throw new ArgumentNullException(nameof(registerUserDto));
            }
            var newUser = _mapper.Map<Person>(registerUserDto);
            newUser.Password = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            await _personRepository.AddPerson(newUser);
        }
        public async Task<string> GenerateJwt(LoginDto loginDto)
        {
            var user = await _personRepository.GetPersonByEmail(loginDto.Email);
            if(user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if(result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid email or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim(ClaimTypes.Email, loginDto.Email)
            };

            if (user is Teacher teacher && teacher.GroupId.HasValue)
            {
                claims.Add(new Claim("GroupId", teacher.GroupId.Value.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
