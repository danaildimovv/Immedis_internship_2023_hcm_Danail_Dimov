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
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserRolesController(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRoles()
        {

            var userRoles = _mapper.Map<List<UserRoleDTO>>(await _userRoleRepository.GetUserRolesAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userRoles.Count == 0)
            {
                return NotFound("No user roles");
            }



            return Ok(userRoles);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRoleById(int id)
        {
            var userRole = _mapper.Map<UserRoleDTO>(await _userRoleRepository.GetUserRoleByIdAsync(id));

            try
            {

                if (userRole == null)
                {
                    return NotFound("No user role");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(userRole);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]

        public async Task<IActionResult> CreateUserRole(UserRoleDTO userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRoleMap = _mapper.Map<UserRole>(userRole);
            var userRoleCreated = await _userRoleRepository.CreateUserRoleAsync(userRoleMap);

            if (!await userRoleCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
    }
}
