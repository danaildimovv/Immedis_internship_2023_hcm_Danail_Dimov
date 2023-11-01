using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

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
    }
}
