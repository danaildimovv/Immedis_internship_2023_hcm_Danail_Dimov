using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationMVC.Controllers
{
    public class CountriesController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public CountriesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> ListCountries()
        {
            List<Country> countries = new();

            HttpResponseMessage response = await _client.GetAsync("Countries");
            if (response.IsSuccessStatusCode)
            {
                countries = await response.Content.ReadAsAsync<List<Country>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(countries);
        }

        [HttpGet]
        public IActionResult AddCountry()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCountry(Country c)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Countries", c);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListCountries", "Countries");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();

        }

    }
}
