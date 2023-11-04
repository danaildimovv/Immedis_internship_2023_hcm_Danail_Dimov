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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {

            var departments = _mapper.Map<List<DepartmentDTO>>(await _departmentRepository.GetDepartmentsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (departments.Count == 0)
            {
                return NotFound("No departments");
            }



            return Ok(departments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = _mapper.Map<DepartmentDTO>(await _departmentRepository.GetDepartmentByIdAsync(id));

            try
            {

                if (department == null)
                {
                    return NotFound("No department");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(department);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("departmentID-{id}/jobs")]
        public async Task<IActionResult> GetJobsByDepartment(int id)
        {
            var jobs = _mapper.Map<List<JobDTO>>(await _departmentRepository.GetJobByDepartmentIdAsync(id));
            return Ok(jobs);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentDTO department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departmentMap = _mapper.Map<Department>(department);
            var departmentCreated = await _departmentRepository.AddDepartmentAsync(departmentMap);

            if (!await departmentCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentDTO department)
        {
            if(id != department.DepartmentId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departmentMap = _mapper.Map<Department>(department);
            var departmentUpdated = _departmentRepository.UpdateDepartmentAsync(departmentMap);

            if (!await departmentUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _departmentRepository.DeleteDepartmentAsync(department))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
}
