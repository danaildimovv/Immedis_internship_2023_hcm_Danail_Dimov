using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<HcmUser>> GetUsersAsync();
        Task<HcmUser> GetUserByIdAsync(int id);
        Task<Task<bool>> CreateUserAsync(HcmUser user);
        Task<bool> UpdateUserAsync(HcmUser user);
        Task<bool> DeleteUserAsync(HcmUser user);
    }
}

