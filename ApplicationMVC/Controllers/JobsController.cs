using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Details(int id)
        {
            Job job = new();

            HttpResponseMessage response = _client.GetAsync("Jobs/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<Job>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(job);
        }
        [HttpGet]
        public async Task<IActionResult> AddJob()
        {
            HttpResponseMessage response = await _client.GetAsync("Departments");
            var contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.DepartmentsList = new SelectList(JsonConvert.DeserializeObject<List<Department>>(contentResponse), "DepartmentId", "DepartmentName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJob(Job j)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Jobs", j);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Jobs");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> UpdateJob(int id)
        {

            Job job = new();
            HttpResponseMessage response = _client.GetAsync("Jobs/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                job = await response.Content.ReadAsAsync<Job>();
                response = await _client.GetAsync("Departments");
                var contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.DepartmentsList = new SelectList(JsonConvert.DeserializeObject<List<Department>>(contentResponse), "DepartmentId", "DepartmentName");
            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(job);

        }
        [HttpPost]
            public async Task<IActionResult> UpdateJob(Job j)
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("Jobs/" + j.JobId, j);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Jobs");
                }

                ModelState.AddModelError("", "Error occured");

                return View();
            }
            [HttpGet]
            public async Task<IActionResult> DeleteJob(int id)
            {
                Job job = new();
                HttpResponseMessage response = _client.GetAsync("Jobs/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    job = await response.Content.ReadAsAsync<Job>();
                }
                return View(job);
            }

            [HttpPost, ActionName("DeleteJob")]
            public async Task<IActionResult> DeleteJobConfirmation(int id)
            {
                HttpResponseMessage response = await _client.DeleteAsync("Jobs/" + id);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Jobs");
                }

                return View();
            }
        }

  
}

