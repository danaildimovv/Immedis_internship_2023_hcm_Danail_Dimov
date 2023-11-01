using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ICountryRepository
    {
        Task<ICollection<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(int id);

        Task<ICollection<Employee>> GetEmployeesByCountryIdAsync(int id);
    }
}
