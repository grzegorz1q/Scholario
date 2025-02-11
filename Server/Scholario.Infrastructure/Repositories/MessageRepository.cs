using Microsoft.EntityFrameworkCore;
using Scholario.Domain.Interfaces.Repositories;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _appDbContext;
        public MessageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            await _appDbContext.Messages.AddAsync(message);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteMessage(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var message = await _appDbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                throw new KeyNotFoundException($"Wiadomość o ID {id} nie został znaleziony.");
            }
            _appDbContext.Messages.Remove(message);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessages()
        {
            return await _appDbContext.Messages.ToListAsync();
        }

        public async Task<Message?> GetMessage(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            _appDbContext.Messages.Update(message);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
