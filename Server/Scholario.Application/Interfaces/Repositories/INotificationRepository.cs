using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task AddNotification(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task GetNotification(int id);
        Task UpdateNotification(Notification notification);
        Task DeleteNotification(int id);
    }
}
