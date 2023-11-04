using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public BranchRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<Branch>> GetBranchesAsync()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<Branch> GetBranchByIdAsync(int id)
        {
            return await _context.Branches.FindAsync(id);
        }
        public async Task<Task<bool>> AddBranchAsync(Branch branch)
        {
            await _context.Branches.AddAsync(branch);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateBranchAsync(Branch branch)
        {
            _context.Branches.Update(branch);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteBranchAsync(Branch branch)
        {
            _context.Branches.Remove(branch);
            return await _genericRepository.SaveAsync();
        }
    }
}
