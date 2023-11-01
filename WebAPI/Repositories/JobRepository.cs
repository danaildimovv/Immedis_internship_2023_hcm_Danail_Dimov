using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly HcmContext _context;
        public JobRepository(HcmContext context)
        {
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
            return SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
