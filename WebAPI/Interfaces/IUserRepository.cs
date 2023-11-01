using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<HcmUser>> GetUsersAsync();
        Task<HcmUser> GetUserByIdAsync(int id);
    }
}

