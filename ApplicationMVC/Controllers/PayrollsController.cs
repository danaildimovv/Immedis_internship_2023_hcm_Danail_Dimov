using ApplicationMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ApplicationMVC.Controllers
{
    public class PayrollsController : Controller
    {
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public PayrollsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Payroll> payrolls = new();

            HttpResponseMessage response = await _client.GetAsync("Payrolls");
            if (response.IsSuccessStatusCode)
            {
                payrolls = await response.Content.ReadAsAsync<List<Payroll>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(payrolls);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Payroll payroll = new();

            HttpResponseMessage response = _client.GetAsync("Payrolls/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                payroll = await response.Content.ReadAsAsync<Payroll>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error.");
            }

            return View(payroll);
        }

        [HttpGet]
        public IActionResult AddPayroll()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPayroll(Payroll p)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("Payrolls", p);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListPayrolls", "Payrolls");
            }
            ModelState.AddModelError(string.Empty, "Error");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePayroll(int id)
        {
            Payroll payroll = new();
            HttpResponseMessage response = _client.GetAsync("Payrolls/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                payroll = await response.Content.ReadAsAsync<Payroll>();
            }
            else
            {
                ModelState.AddModelError("", "Error occured");
            }

            return View(payroll);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePayroll(Payroll p)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync("Payrolls/" + p.PayrollId, p);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Payrolls");
            }

            ModelState.AddModelError("", "Error occured");

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeletePayroll(int id)
        {
            Payroll payroll = new();
            HttpResponseMessage response = _client.GetAsync("Payrolls/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                payroll = await response.Content.ReadAsAsync<Payroll>();
            }
            return View(payroll);
        }

        [HttpPost, ActionName("DeletePayroll")]
        public async Task<IActionResult> DeletePayrollConfirmation(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync("Payrolls/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Payrolls");
            }

            return View();
        }
    }
}
