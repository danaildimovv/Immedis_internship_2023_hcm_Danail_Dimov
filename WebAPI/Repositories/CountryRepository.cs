using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly HcmContext _context;
        private readonly IGenericRepository _genericRepository;
        public CountryRepository(HcmContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
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

        public async Task<Task<bool>> AddCountryAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
            return _genericRepository.SaveAsync();
        }
        public async Task<bool> UpdateCountryAsync(Country country)
        {
            _context.Countries.Update(country);
            return await _genericRepository.SaveAsync();
        }
        public async Task<bool> DeleteCountryAsync(Country country)
        {
            _context.Countries.Remove(country);
            return await _genericRepository.SaveAsync();
        }
    }
}
