using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
    }
}
