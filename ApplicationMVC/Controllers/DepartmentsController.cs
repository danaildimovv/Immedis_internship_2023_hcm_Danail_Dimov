using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ApplicationMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public DepartmentsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Department> departments = new();

            HttpResponseMessage response = await _client.GetAsync("Departments");
            if (response.IsSuccessStatusCode)
            {
                departments = await response.Content.ReadAsAsync<List<Department>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(departments);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Department department = new();

            HttpResponseMessage response = _client.GetAsync("Departments/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                department = await response.Content.ReadAsAsync<Department>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department d)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Departments", d);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListDepartments", "Departments");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateDepartment(int id)
        {
            Department department = new();
            HttpResponseMessage response = _client.GetAsync("Departments/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                department = await response.Content.ReadAsAsync<Department>();
            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDepartment(Department d)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("Departments/" + d.DepartmentId, d);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Departments");
            }

            ModelState.AddModelError("", "Error occured");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department department = new();
            HttpResponseMessage response = _client.GetAsync("Departments/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                department = await response.Content.ReadAsAsync<Department>();
            }
            return View(department);
        }

        [HttpPost, ActionName("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartmentConfirmation(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("Departments/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Departments");
            }

            return View();
        }
    }
}
