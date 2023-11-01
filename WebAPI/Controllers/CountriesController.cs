using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public CountriesController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {

            var countries = _mapper.Map<List<CountryDTO>>(await _countryRepository.GetCountriesAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (countries.Count == 0)
            {
                return NotFound("No countries");
            }



            return Ok(countries);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            var country = _mapper.Map<CountryDTO>(await _countryRepository.GetCountryByIdAsync(id));

            try
            {

                if (country == null)
                {
                    return NotFound("No country with the given id");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(country);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("country-{id}")]
        public async Task<IActionResult> GetEmployeesByCountry(int id)
        {
            var employees = _mapper.Map<List<EmployeeDTO>>(await _countryRepository.GetEmployeesByCountryIdAsync(id));

            return Ok(employees);
        }
    }
}
