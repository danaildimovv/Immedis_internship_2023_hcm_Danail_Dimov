using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly HcmContext _context;
        public JobRepository(IGenericRepository genericRepository, HcmContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<ICollection<Job>> GetJobsAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<Job> GetJobByIdAsync(int id)
        {
            return await _context.Jobs.FindAsync(id);
        }

        public async Task<Task<bool>> CreateJobAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateJobAsync(Job job)
        {
            _context.Jobs.Update(job);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteJobAsync(Job job)
        {
            _context.Jobs.Remove(job);
            return await _genericRepository.SaveAsync();
        }

    }
}
