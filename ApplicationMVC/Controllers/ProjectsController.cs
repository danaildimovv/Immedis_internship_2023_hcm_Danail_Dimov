using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ApplicationMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public ProjectsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Project> projects = new();

            HttpResponseMessage response = await _client.GetAsync("Projects");
            if (response.IsSuccessStatusCode)
            {
                projects = await response.Content.ReadAsAsync<List<Project>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(projects);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Project project = new();

            HttpResponseMessage response = _client.GetAsync("Projects/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                project = await response.Content.ReadAsAsync<Project>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(project);
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProject(Project p)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Projects", p);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Projects");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProject(int id)
        {
            Project project = new();
            HttpResponseMessage response = _client.GetAsync("Projects/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                project = await response.Content.ReadAsAsync<Project>();                
            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(project);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project p)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("Projects/" + p.ProjectId, p);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Projects");
            }

            ModelState.AddModelError("", "Error occured");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProject(int id)
        {
            Project project = new();
            HttpResponseMessage response = _client.GetAsync("Projects/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                project = await response.Content.ReadAsAsync<Project>();
            }
            return View(project);
        }

        [HttpPost, ActionName("DeleteProject")]
        public async Task<IActionResult> DeleteProjectConfirmation(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("Projects/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Projects");
            }

            return View();
        }
    }
}
