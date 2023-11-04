using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public UserRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }

        public async Task<ICollection<HcmUser>> GetUsersAsync()
        {
            return await _context.HcmUsers.ToListAsync();
        }

        public async Task<HcmUser> GetUserByIdAsync(int id)
        {
            return await _context.HcmUsers.FindAsync(id);
        }
        public async Task<Task<bool>> CreateUserAsync(HcmUser user)
        {
            await _context.HcmUsers.AddAsync(user);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateUserAsync(HcmUser user)
        {
            _context.HcmUsers.Update(user);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteUserAsync(HcmUser user)
        {
            _context.HcmUsers.Remove(user);
            return await _genericRepository.SaveAsync();
        }
    }
}
