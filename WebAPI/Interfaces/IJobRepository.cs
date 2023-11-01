using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IJobRepository
    {
        Task<ICollection<Job>> GetJobsAsync();
        Task<Job> GetJobByIdAsync(int id);

        Task<Task<bool>> CreateJobAsync(Job job);
        Task<bool> SaveAsync();
    }
}
