using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public TrainingRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<Training>> GetTrainingsAsync()
        {
            return await _context.Trainings.ToListAsync();
        }

        public async Task<Training> GetTrainingByIdAsync(int id)
        {
            return await _context.Trainings.FindAsync(id);
        }
        public async Task<Task<bool>> AddTrainingAsync(Training training)
        {
            await _context.Trainings.AddAsync(training);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateTrainingAsync(Training training)
        {
            _context.Trainings.Update(training);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteTrainingAsync(Training training)
        {
            _context.Trainings.Remove(training);
            return await _genericRepository.SaveAsync();
        }
    }
}
