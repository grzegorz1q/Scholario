using Scholario.Application.Dtos.ClassRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface IClassroomService
    {
        Task<IEnumerable<ReadClassRoomDto>> GetClassrooms();
    }
}
