using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly HcmContext _context;
        public TrainingRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Training>> GetTrainingsAsync()
        {
            return await _context.Trainings.ToListAsync();
        }

        public async Task<Training> GetTrainingByIdAsync(int id)
        {
            return await _context.Trainings.FindAsync(id);
        }
    }
}
