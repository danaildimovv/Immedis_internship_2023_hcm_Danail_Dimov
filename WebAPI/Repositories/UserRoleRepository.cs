using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public UserRoleRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<UserRole>> GetUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }
        public async Task<Task<bool>> CreateUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Remove(userRole);
            return await _genericRepository.SaveAsync();
        }
    }
}
