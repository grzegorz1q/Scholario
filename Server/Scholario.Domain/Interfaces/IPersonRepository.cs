using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task AddPerson(Person person);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person?> GetPersonByEmail(string email);
    }
}
