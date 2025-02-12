using Scholario.Application.Dtos;
using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
    }
}
