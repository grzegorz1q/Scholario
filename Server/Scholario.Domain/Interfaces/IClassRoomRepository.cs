using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Domain.Interfaces
{
    public interface IClassroomRepository
    {
        Task<IEnumerable<Classroom>> GetClassrooms();
        Task<Classroom?> GetClassroomByNumber(int number);
        Task<bool> IsClassroomOccupied(int classroomId, DayOfWeek day, int lessonNumber);
    }
}
