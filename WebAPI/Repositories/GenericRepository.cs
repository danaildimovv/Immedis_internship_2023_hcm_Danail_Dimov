using WebAPI.Data;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private readonly HcmContext _context;
        public GenericRepository(HcmContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
