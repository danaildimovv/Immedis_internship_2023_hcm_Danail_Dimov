using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ExperienceLevelRepository : IExperienceLevelRepository
    {
        private readonly HcmContext _context;
        public ExperienceLevelRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ExperienceLevel>> GetExperienceLevelsAsync()
        {
            return await _context.ExperienceLevels.ToListAsync();
        }

        public async Task<ExperienceLevel> GetExperienceLevelByIdAsync(int id)
        {
            return await _context.ExperienceLevels.FindAsync(id);
        }
    }
}
