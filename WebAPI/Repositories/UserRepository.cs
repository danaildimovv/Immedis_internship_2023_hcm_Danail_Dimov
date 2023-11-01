using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HcmContext _context;
        public UserRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<HcmUser>> GetUsersAsync()
        {
            return await _context.HcmUsers.ToListAsync();
        }

        public async Task<HcmUser> GetUserByIdAsync(int id)
        {
            return await _context.HcmUsers.FindAsync(id);
        }
    }
}
