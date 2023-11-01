using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly HcmContext _context;
        public ProjectRepository(HcmContext context)
        {
            _context = context;
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
            return SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
