using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task<IEnumerable<Message>> GetAllMessages();
        Task<Message?> GetMessage(int id);
        Task UpdateMessage(Message message);
        Task DeleteMessage(int id);
    }
}
