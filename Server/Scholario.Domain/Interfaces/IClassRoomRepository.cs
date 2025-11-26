using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IClassRoomRepository
    {
        Task<IEnumerable<Classroom>> GetClassrooms();
    }
}
