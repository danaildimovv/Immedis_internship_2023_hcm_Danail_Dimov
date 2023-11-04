using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IExperienceLevelRepository
    {
        Task<ICollection<ExperienceLevel>> GetExperienceLevelsAsync();

        Task<ExperienceLevel> GetExperienceLevelByIdAsync(int id);

        Task<Task<bool>> AddExperienceLevelAsync(ExperienceLevel experienceLevel);
        Task<bool> UpdateExperienceLevelAsync(ExperienceLevel experienceLevel);
        Task<bool> DeleteExperienceLevelAsync(ExperienceLevel experienceLevel);
    }
}
