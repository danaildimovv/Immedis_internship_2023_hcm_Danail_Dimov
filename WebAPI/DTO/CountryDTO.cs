using WebAPI.Models;

namespace WebAPI.DTO
{
    public class CountryDTO
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
