using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IBranchRepository
    {
        Task<ICollection<Branch>> GetBranchesAsync();
        Task<Branch> GetBranchByIdAsync(int id);

        Task<Task<bool>> AddBranchAsync(Branch branch);
        Task<bool> UpdateBranchAsync(Branch branch);
        Task<bool> DeleteBranchAsync(Branch branch);

    }
}
