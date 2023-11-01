using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly HcmContext _context;
        public PayrollRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Payroll>> GetPayrollsAsync()
        {
            return await _context.Payrolls.ToListAsync();
        }

        public async Task<Payroll> GetPayrollByIdAsync(int id)
        {
            return await _context.Payrolls.FindAsync(id);
        }
    }
}
