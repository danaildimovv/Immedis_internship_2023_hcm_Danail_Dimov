using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<ICollection<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<ICollection<Job>> GetJobByDepartmentIdAsync(int id);
        Task<Task<bool>> AddDepartmentAsync(Department department);
        Task<bool> UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(Department department);
    }
}
