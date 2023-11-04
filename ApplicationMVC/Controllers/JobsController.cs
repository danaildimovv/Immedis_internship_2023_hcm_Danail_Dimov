using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationMVC.Controllers
{
    public class JobsController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public JobsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> ListJobs()
        {
            List<Job> jobs = new();

            HttpResponseMessage response = await _client.GetAsync("Jobs");
            if (response.IsSuccessStatusCode)
            {
                jobs = await response.Content.ReadAsAsync<List<Job>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(jobs);
        }

        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(Job j)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Jobs", j);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListJobs", "Jobs");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();

        }

    }
}

