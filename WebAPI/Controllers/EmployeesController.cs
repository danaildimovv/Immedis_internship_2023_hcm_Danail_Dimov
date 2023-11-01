using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            
            var employees = _mapper.Map<List<EmployeeDTO>>(await _employeeRepository.GetEmployeesAsync());
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (employees.Count == 0)
            {
                return NotFound("No employees");
            }



            return Ok(employees);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id) {
            var employee = _mapper.Map<EmployeeDTO>(await _employeeRepository.GetEmployeeByIdAsync(id));

            try { 
                
                if (employee == null) {
                    return NotFound("No employee");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(employee);
            }

            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }
        /*
        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            try { 
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Ok("New employee was added.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("wrong employee change");
            }

            var emp = await _context.Employees.FindAsync(employee.EmployeeId);
            
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

                await _context.SaveChangesAsync();

                return Ok("change");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            try { 
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return Ok("delete");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        */

    }
}
