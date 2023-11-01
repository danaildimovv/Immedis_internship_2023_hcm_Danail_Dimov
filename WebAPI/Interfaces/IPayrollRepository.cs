using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IPayrollRepository
    {
        Task<ICollection<Payroll>> GetPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(int id);
    }
}
