using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<ICollection<UserRole>> GetUserRolesAsync();

        Task<UserRole> GetUserRoleByIdAsync(int id);
    }
}
