using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IGenericRepository _genericRepository;
        private readonly HcmContext _context;
        public DepartmentRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
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
        public async Task<Task<bool>> AddDepartmentAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteDepartmentAsync(Department department)
        {
            _context.Departments.Remove(department);
            return await _genericRepository.SaveAsync();
        }
    }
}
