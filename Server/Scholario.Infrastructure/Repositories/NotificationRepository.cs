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
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _appDbContext;
        public NotificationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddNotification(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            await _appDbContext.Notifications.AddAsync(notification);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteNotification(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var notification = await _appDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
            if (notification == null)
            {
                throw new KeyNotFoundException($"Powiadomienie o ID {id} nie został znaleziony.");
            }
            _appDbContext.Notifications.Remove(notification);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _appDbContext.Notifications.ToListAsync();
        }

        public async Task<Notification?> GetNotification(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task UpdateNotification(Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));
            _appDbContext.Notifications.Update(notification);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
