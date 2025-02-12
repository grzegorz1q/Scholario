using FluentValidation;
using Scholario.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Dtos.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(IPersonRepository personRepository) 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            /*    .MustAsync(async (email, cancelation) =>     //Nie dziala. Zadziala sprawdzanie ręcznie w serwisie ale dobrze by było, żeby działało tutaj
                {
                    var allPersons = await personRepository.GetAllPersons();
                    return !allPersons.Any(p => p.Email == email);
                })
                .WithMessage("That email is taken");*/

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);


        }

    }
}
