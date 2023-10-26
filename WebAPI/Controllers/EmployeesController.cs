using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly HcmContext _context;

        public EmployeesController(HcmContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _context.Employees.ToList();
            if (employees.Count == 0)
            {
                return NotFound("No employees");
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var employee = _context.Employees.Find(id);

            try { 
                if (employee == null) {
                    return NotFound("No employee");
                }

                return Ok(employee);
            }

            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try { 
                _context.Add(employee);
                _context.SaveChanges();
                return Ok("added.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("wrong employee change");
            }

            var emp = _context.Employees.Find(employee.EmployeeId);
            if (emp == null)
            {
                return NotFound("Not Found.");
            }

            try { 
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;
                emp.JobId = employee.JobId;
                emp.ProjectId = employee.ProjectId;
                emp.ExperienceLevelId = employee.ExperienceLevelId;
                emp.EducationLevelId = employee.EducationLevelId;
                emp.PayrollId = employee.PayrollId;
                emp.BranchId = employee.BranchId;
                emp.Email = employee.Email;
                emp.UserId = employee.UserId;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.EmployeeAddress = employee.EmployeeAddress;
                emp.NationalityId = employee.NationalityId;
                emp.Gender = employee.Gender;
                emp.DateOfEmployment = employee.DateOfEmployment;
                emp.DateOfLeaving = employee.DateOfLeaving;
                _context.SaveChanges();

                return Ok("change");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            try { 
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return Ok("delete");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
