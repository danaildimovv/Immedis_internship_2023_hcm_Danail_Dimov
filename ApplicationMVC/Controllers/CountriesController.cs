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
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Details(int id)
        {
            Country country = new();

            HttpResponseMessage response = _client.GetAsync("Countries/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                country = await response.Content.ReadAsAsync<Country>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(country);
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
            try { 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Country successfully added";
                    return RedirectToAction("Index", "Countries");
                }
            }
            catch(Exception e) {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCountry(int id)
        {
            Country country = new();
            HttpResponseMessage response = _client.GetAsync("Countries/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                country = await response.Content.ReadAsAsync<Country>();
            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(country);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCountry(Country c)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("Countries/" + c.CountryId, c);
            try { 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Country updated";
                    return RedirectToAction("Index", "Departments");
                }
            }
            catch(Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            Country country = new();
            HttpResponseMessage response = _client.GetAsync("Countries/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                country = await response.Content.ReadAsAsync<Country>();
            }
            return View(country);
        }

        [HttpPost, ActionName("DeleteCountry")]
        public async Task<IActionResult> DeleteCountryConfirmation(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("Countries/" + id);
            try { 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Country deleted";
                    return RedirectToAction("Index", "Countries");
                }
            }
            catch (Exception e) 
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }

            return View();
        }

    }
}
