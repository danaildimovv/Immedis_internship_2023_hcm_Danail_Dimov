using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class EducationLevelRepository : IEducationLevelRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public EducationLevelRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<EducationLevel>> GetEducationLevelsAsync()
        {
            return await _context.EducationLevels.ToListAsync();
        }

        public async Task<EducationLevel> GetEducationLevelByIdAsync(int id)
        {
            return await _context.EducationLevels.FindAsync(id);
        }
        public async Task<Task<bool>> AddEducationLevelAsync(EducationLevel educationLevel)
        {
            await _context.EducationLevels.AddAsync(educationLevel);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateEducationLevelAsync(EducationLevel educationLevel)
        {
            _context.EducationLevels.Update(educationLevel);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteEducationLevelAsync(EducationLevel educationLevel)
        {
            _context.EducationLevels.Remove(educationLevel);
            return await _genericRepository.SaveAsync();
        }
    }
}
