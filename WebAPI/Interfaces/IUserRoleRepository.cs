using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<ICollection<UserRole>> GetUserRolesAsync();

        Task<UserRole> GetUserRoleByIdAsync(int id);
        Task<Task<bool>> CreateUserRoleAsync(UserRole userRole);
        Task<bool> UpdateUserRoleAsync(UserRole userRole);
        Task<bool> DeleteUserRoleAsync(UserRole userRole);
    }
}
