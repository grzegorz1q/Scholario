using Microsoft.EntityFrameworkCore;
using Scholario.Application.Interfaces.Repositories;
using Scholario.Domain.Entities;
using Scholario.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scholario.Infrastructure.Repositories
{
    public class DescriptiveAssessmentRepository : IDescriptiveAssessmentRepository
    {
        private readonly AppDbContext _appDbContext;
        public DescriptiveAssessmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddDescriptiveAssessment(DescriptiveAssessment descriptiveAssessment)
        {
            if (descriptiveAssessment == null)
                throw new ArgumentNullException(nameof(descriptiveAssessment));
            await _appDbContext.DescriptiveAssessments.AddAsync(descriptiveAssessment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteDescriptiveAssessment(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            var descriptiveAssessment = await _appDbContext.DescriptiveAssessments.FirstOrDefaultAsync(d => d.Id == id);
            if (descriptiveAssessment == null)
            {
                throw new KeyNotFoundException($"Ocena opisowa o ID {id} nie została znaleziona.");
            }
            _appDbContext.DescriptiveAssessments.Remove(descriptiveAssessment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DescriptiveAssessment>> GetAllDescriptiveAssessments()
        {
            return await _appDbContext.DescriptiveAssessments.ToListAsync();
        }

        public async Task<DescriptiveAssessment?> GetDescriptiveAssessment(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            return await _appDbContext.DescriptiveAssessments.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateDescriptiveAssessment(DescriptiveAssessment descriptiveAssessment)
        {
            if (descriptiveAssessment == null)
                throw new ArgumentNullException(nameof(descriptiveAssessment));
            _appDbContext.DescriptiveAssessments.Update(descriptiveAssessment);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
