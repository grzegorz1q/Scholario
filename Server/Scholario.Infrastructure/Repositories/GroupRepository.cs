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
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _appDbContext;
        public GroupRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddGroup(Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            await _appDbContext.Groups.AddAsync(group);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteGroup(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var group = await _appDbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
            {
                throw new KeyNotFoundException($"Grupa o ID {id} nie została znaleziona.");
            }
            _appDbContext.Groups.Remove(group);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _appDbContext.Groups.ToListAsync();
        }

        public async Task<Group?> GetGroup(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task UpdateGroup(Group group)
        {
            if (group == null)
                throw new ArgumentNullException(nameof(group));
            _appDbContext.Groups.Update(group);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
