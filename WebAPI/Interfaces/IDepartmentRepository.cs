using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<ICollection<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<ICollection<Job>> GetJobByDepartmentIdAsync(int id);
    }
}
