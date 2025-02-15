using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Entities;
using Scholario.Domain.Interfaces;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _appDbContext;
        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Person?> GetPerson(int id)
        {
            return await _appDbContext.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddPerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            await _appDbContext.Persons.AddAsync(person);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _appDbContext.Persons.ToListAsync();
        }

        public async Task<Person?> GetPersonByEmail(string email)
        {
            return await _appDbContext.Persons.FirstOrDefaultAsync(p => p.Email == email);
        }
    }
}
