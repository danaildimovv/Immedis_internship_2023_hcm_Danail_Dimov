using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApplicationMVC.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Details(int id)
        {
            Employee employee = new ();

            HttpResponseMessage response = _client.GetAsync("Employees/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                employee = await response.Content.ReadAsAsync<Employee>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            HttpResponseMessage response = await _client.GetAsync("Jobs");
            var contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.JobsList = new SelectList(JsonConvert.DeserializeObject<List<Job>>(contentResponse), "JobId", "JobTitle");

            response = await _client.GetAsync("Projects");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.ProjectsList = new SelectList(JsonConvert.DeserializeObject<List<Project>>(contentResponse), "ProjectId", "ProjectName");

            response = await _client.GetAsync("ExperienceLevels");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.ExperienceLevelsList = new SelectList(JsonConvert.DeserializeObject<List<ExperienceLevel>>(contentResponse), "ExperienceLevelId", "ExperienceLevelTitle");

            response = await _client.GetAsync("EducationLevels");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.EducationLevelsList = new SelectList(JsonConvert.DeserializeObject<List<EducationLevel>>(contentResponse), "EducationLevelId", "EducationLevelTitle");
            
            response = await _client.GetAsync("Payrolls");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.PayrollsList = new SelectList(JsonConvert.DeserializeObject<List<Payroll>>(contentResponse), "PayrollId", "PayrollId");

            response = await _client.GetAsync("Branches");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.BranchesList = new SelectList(JsonConvert.DeserializeObject<List<Branch>>(contentResponse), "BranchId", "BranchName");

            response = await _client.GetAsync("Countries");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.CountriesList = new SelectList(JsonConvert.DeserializeObject<List<Country>>(contentResponse), "CountryId", "CountryName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee em)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Employees", em);
            try { 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee created successfully";
                    return RedirectToAction("Index", "Employees"); 
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
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            Employee employee = new ();
            HttpResponseMessage response = _client.GetAsync("Employees/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                employee = await response.Content.ReadAsAsync<Employee>();
                response = await _client.GetAsync("Jobs");
                var contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.JobsList = new SelectList(JsonConvert.DeserializeObject<List<Job>>(contentResponse), "JobId", "JobTitle");

                response = await _client.GetAsync("Projects");
                contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.ProjectsList = new SelectList(JsonConvert.DeserializeObject<List<Project>>(contentResponse), "ProjectId", "ProjectName");

                response = await _client.GetAsync("ExperienceLevels");
                contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.ExperienceLevelsList = new SelectList(JsonConvert.DeserializeObject<List<ExperienceLevel>>(contentResponse), "ExperienceLevelId", "ExperienceLevelTitle");

                response = await _client.GetAsync("EducationLevels");
                contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.EducationLevelsList = new SelectList(JsonConvert.DeserializeObject<List<EducationLevel>>(contentResponse), "EducationLevelId", "EducationLevelTitle");
            
                response = await _client.GetAsync("Payrolls");
                contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.PayrollsList = new SelectList(JsonConvert.DeserializeObject<List<Payroll>>(contentResponse), "PayrollId", "PayrollId");

                response = await _client.GetAsync("Branches");
                contentResponse = await response.Content.ReadAsStringAsync();
                ViewBag.BranchesList = new SelectList(JsonConvert.DeserializeObject<List<Branch>>(contentResponse), "BranchId", "BranchName");

            response = await _client.GetAsync("Countries");
            contentResponse = await response.Content.ReadAsStringAsync();
            ViewBag.CountriesList = new SelectList(JsonConvert.DeserializeObject<List<Country>>(contentResponse), "CountryId", "CountryName");

            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("Employees/" + employee.EmployeeId, employee);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee updated successfully";
                    return RedirectToAction("Index", "Employees");
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
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Employee employee = new ();
            HttpResponseMessage response = _client.GetAsync("Employees/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                employee = await response.Content.ReadAsAsync<Employee>();
            }
            return View(employee);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeConfirmation(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("Employees/" + id);
            try { 
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee deleted successfully";
                    return RedirectToAction("Index", "Employees");
                }
            }
            catch(Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }

    }
}
