using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IProjectRepository
    {
        Task<ICollection<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
        Task<Task<bool>> CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Project project);
    }
}
