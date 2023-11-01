using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class EducationLevelRepository : IEducationLevelRepository
    {
        private readonly HcmContext _context;
        public EducationLevelRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<EducationLevel>> GetEducationLevelsAsync()
        {
            return await _context.EducationLevels.ToListAsync();
        }

        public async Task<EducationLevel> GetEducationLevelByIdAsync(int id)
        {
            return await _context.EducationLevels.FindAsync(id);
        }
    }
}
