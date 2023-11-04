using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

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
        [HttpPost]
        public async Task<IActionResult> AddCountry(CountryDTO country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(country);
            var countryCreated = await _countryRepository.AddCountryAsync(countryMap);

            if (!await countryCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(int id, CountryDTO country)
        {
            if(id != country.CountryId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var countryMap = _mapper.Map<Country>(country);
            var countryUpdated = _countryRepository.UpdateCountryAsync(countryMap);

            if (!await countryUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryRepository.GetCountryByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _countryRepository.DeleteCountryAsync(country))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");
           
        }
    }
}
