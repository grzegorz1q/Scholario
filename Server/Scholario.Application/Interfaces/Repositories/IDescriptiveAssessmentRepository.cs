using Scholario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Application.Interfaces.Repositories
{
    public interface IDescriptiveAssessmentRepository
    {
        Task AddDescriptiveAssessment(DescriptiveAssessment descriptiveAssessment);
        Task<IEnumerable<DescriptiveAssessment>> GetAllDescriptiveAssessments();
        Task GetDescriptiveAssessment(int id);
        Task UpdateDescriptiveAssessment(DescriptiveAssessment descriptiveAssessment);
        Task DeleteDescriptiveAssessment(int id);
    }
}
