using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public PayrollRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<Payroll>> GetPayrollsAsync()
        {
            return await _context.Payrolls.ToListAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }
        public async Task<Task<bool>> AddPayrollAsync(Payroll payroll)
        {
            await _context.Payrolls.AddAsync(payroll);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdatePayrollAsync(Payroll payroll)
        {
            _context.Payrolls.Update(payroll);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeletePayrollAsync(Payroll payroll)
        {
            _context.Payrolls.Remove(payroll);
            return await _genericRepository.SaveAsync();
        }
    }
}
