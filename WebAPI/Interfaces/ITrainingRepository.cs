using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ITrainingRepository
    {
        Task<ICollection<Training>> GetTrainingsAsync();
        Task<Training> GetTrainingByIdAsync(int id);
        Task<Task<bool>> AddTrainingAsync(Training training);
        Task<bool> UpdateTrainingAsync(Training training);
        Task<bool> DeleteTrainingAsync(Training training);
    }
}
