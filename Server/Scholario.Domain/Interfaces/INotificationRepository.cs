using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task AddNotification(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification?> GetNotification(int id);
        Task UpdateNotification(Notification notification);
        Task DeleteNotification(int id);
    }
}
