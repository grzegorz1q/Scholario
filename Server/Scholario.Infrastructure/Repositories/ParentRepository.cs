using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Interfaces;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly AppDbContext _appDbContext;
        public ParentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddParent(Parent parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            await _appDbContext.Persons.AddAsync(parent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteParent(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var parent = await _appDbContext.Persons.OfType<Parent>().FirstOrDefaultAsync(p => p.Id == id);
            if (parent == null)
            {
                throw new KeyNotFoundException($"Rodzic o ID {id} nie został znaleziony.");
            }
            _appDbContext.Persons.Remove(parent);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Parent>> GetAllParents()
        {
            return await _appDbContext.Persons.OfType<Parent>().ToListAsync();
        }

        public async Task<Parent?> GetParent(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Persons.OfType<Parent>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateParent(Parent parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));
            _appDbContext.Persons.Update(parent);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
