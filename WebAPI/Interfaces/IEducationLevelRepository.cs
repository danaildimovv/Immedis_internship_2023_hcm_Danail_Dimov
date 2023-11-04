using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IEducationLevelRepository
    {
        Task<ICollection<EducationLevel>> GetEducationLevelsAsync();

        Task<EducationLevel> GetEducationLevelByIdAsync(int id);

        Task<Task<bool>> AddEducationLevelAsync(EducationLevel educationLevel);
        Task<bool> UpdateEducationLevelAsync(EducationLevel educationLevel);
        Task<bool> DeleteEducationLevelAsync(EducationLevel educationLevel);
    }
}
