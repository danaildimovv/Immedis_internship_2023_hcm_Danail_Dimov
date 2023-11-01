using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HcmContext _context;
        public CountryRepository(HcmContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<ICollection<Employee>> GetEmployeesByCountryIdAsync(int id)
        {
            return await _context.Employees.Where(r => r.Nationality.CountryId == id).ToListAsync();
        }

    }
}
