using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HcmContext _context;
        public DepartmentRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<ICollection<Job>> GetJobByDepartmentIdAsync(int id)
        {
            return await _context.Jobs.Where(j => j.DepartmentId == id).ToListAsync();
        }
    }
}
