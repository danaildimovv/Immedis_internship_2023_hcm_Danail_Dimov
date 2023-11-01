using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ITrainingRepository
    {
        Task<ICollection<Training>> GetTrainingsAsync();
        Task<Training> GetTrainingByIdAsync(int id);
    }
}
