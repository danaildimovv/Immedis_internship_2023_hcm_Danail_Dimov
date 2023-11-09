using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IExperienceLevelRepository _experienceLevelRepository;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper, IExperienceLevelRepository experienceLevelRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _experienceLevelRepository = experienceLevelRepository;
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
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeMap = _mapper.Map<Employee>(employee);
            var employeeCreated = await _employeeRepository.AddEmployeeAsync(employeeMap);

            if (!await employeeCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeMap = _mapper.Map<Employee>(employee);
            var employeeUpdated = _employeeRepository.UpdateEmployeeAsync(employeeMap);

            if (!await employeeUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _employeeRepository.DeleteEmployeeAsync(employee))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
}
