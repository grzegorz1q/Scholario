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
    public class ClassRoomRepository : IClassRoomRepository
    {
        private readonly AppDbContext _appDbContext;
        public ClassRoomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Classroom>> GetClassrooms()
        {
            return await _appDbContext.Classrooms.ToListAsync();
        }
    }
}
