﻿using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        Task AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetAllSubjects();
        Task GetSubject(int id);
        Task UpdateSubject(Subject subject);
        Task DeleteSubject(int id);
    }
}
