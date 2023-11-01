using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = _mapper.Map<List<UserDTO>>(await _userRepository.GetUsersAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (users.Count == 0)
            {
                return NotFound("No users");
            }



            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = _mapper.Map<UserDTO>(await _userRepository.GetUserByIdAsync(id));

            try
            {

                if (user == null)
                {
                    return NotFound("No user with id");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(user);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
