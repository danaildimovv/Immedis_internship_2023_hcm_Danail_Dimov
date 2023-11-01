using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IExperienceLevelRepository
    {
        Task<ICollection<ExperienceLevel>> GetExperienceLevelsAsync();

        Task<ExperienceLevel> GetExperienceLevelByIdAsync(int id);
    }
}
