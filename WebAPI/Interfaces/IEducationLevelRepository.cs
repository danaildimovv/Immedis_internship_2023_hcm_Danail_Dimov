using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IEducationLevelRepository
    {
        Task<ICollection<EducationLevel>> GetEducationLevelsAsync();

        Task<EducationLevel> GetEducationLevelByIdAsync(int id);
    }
}
