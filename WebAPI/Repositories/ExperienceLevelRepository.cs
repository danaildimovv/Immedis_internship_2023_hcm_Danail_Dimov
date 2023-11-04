using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ExperienceLevelRepository : IExperienceLevelRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public ExperienceLevelRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<ExperienceLevel>> GetExperienceLevelsAsync()
        {
            return await _context.ExperienceLevels.ToListAsync();
        }

        public async Task<ExperienceLevel> GetExperienceLevelByIdAsync(int id)
        {
            return await _context.ExperienceLevels.FindAsync(id);
        }

        public async Task<Task<bool>> AddExperienceLevelAsync(ExperienceLevel experienceLevel)
        {
            await _context.ExperienceLevels.AddAsync(experienceLevel);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateExperienceLevelAsync(ExperienceLevel experienceLevel)
        {
            _context.ExperienceLevels.Update(experienceLevel);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteExperienceLevelAsync(ExperienceLevel experienceLevel)
        {
            _context.ExperienceLevels.Remove(experienceLevel);
            return await _genericRepository.SaveAsync();
        }
    }
}
