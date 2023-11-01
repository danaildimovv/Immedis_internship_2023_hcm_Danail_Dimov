using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly HcmContext _context;
        public UserRoleRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserRole>> GetUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }
    }
}
