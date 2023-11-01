using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IBranchRepository
    {
        Task<ICollection<Branch>> GetBranchesAsync();
        Task<Branch> GetBranchByIdAsync(int id);
    }
}
