using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public ProjectRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<Project>> GetProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<Task<bool>> CreateProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteProjectAsync(Project project)
        {
            _context.Projects.Remove(project);
            return await _genericRepository.SaveAsync();
        }
    }
}
