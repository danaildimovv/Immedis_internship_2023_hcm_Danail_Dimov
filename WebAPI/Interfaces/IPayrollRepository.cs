using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IPayrollRepository
    {
        Task<ICollection<Payroll>> GetPayrollsAsync();
        Task<Payroll> GetPayrollByIdAsync(int id);
        Task<Task<bool>> AddPayrollAsync(Payroll payroll);
        Task<bool> UpdatePayrollAsync(Payroll payroll);
        Task<bool> DeletePayrollAsync(Payroll payroll);
    }
}
