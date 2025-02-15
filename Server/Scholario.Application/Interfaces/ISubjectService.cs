using Scholario.Application.Dtos.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces
{
    public interface ISubjectService
    {
        Task CreateSubject(CreateSubjectDto createSubjectDto);
        Task<UserSubjectsDto> GetLoggedUserSubjects(int userId);
    }
}
