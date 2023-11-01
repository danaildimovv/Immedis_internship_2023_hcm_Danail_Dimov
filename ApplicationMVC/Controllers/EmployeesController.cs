using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApplicationMVC.Models;

namespace ApplicationMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Uri baseAddress = new ("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public EmployeesController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> ListEmployees()
        {
            List<Employee> employees = new ();
        
                HttpResponseMessage response = await _client.GetAsync("Employees");
                if (response.IsSuccessStatusCode)
                {
                    employees = await response.Content.ReadAsAsync<List<Employee>>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error.");
                }
      
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee em)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Employees", em);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListEmployees", "Employees"); 
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();

        }

    }
}
