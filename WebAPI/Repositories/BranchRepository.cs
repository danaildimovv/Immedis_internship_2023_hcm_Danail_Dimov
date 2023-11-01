using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly HcmContext _context;
        public BranchRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Branch>> GetBranchesAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<Branch> GetBranchByIdAsync(int id)
        {
            return await _context.Branches.FindAsync(id);
        }
    }
}
