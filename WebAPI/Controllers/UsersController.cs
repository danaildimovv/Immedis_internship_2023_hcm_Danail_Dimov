using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = _mapper.Map<List<UserDetailsDTO>>(await _userRepository.GetUsersAsync());

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
            var user = _mapper.Map<UserDetailsDTO>(await _userRepository.GetUserByIdAsync(id));

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
        [HttpPost("/register")]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<HcmUser>(user);
            userMap.Username = user.Username;
            userMap.PasswordHash = passwordHash;
            userMap.PasswordSalt = passwordSalt;
            userMap.UserRole = await _userRoleRepository.GetUserRoleByIdAsync(userMap.UserRoleId);

            var userCreated = await _userRepository.CreateUserAsync(userMap);

            if (!await userCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(UserLoginDTO request)
        {
            var users = await _userRepository.GetUsersAsync();
            byte [] userPasswordHash;
            byte [] userPasswordSalt;
            string token = "";

            var userExists = false;

            foreach (var user in users)
            {
                if (user.Username == request.Username) { 
                    userExists = true;
                    userPasswordHash = user.PasswordHash;
                    userPasswordSalt = user.PasswordSalt;
                    if (!VerifyPasswordHash(request.Password, userPasswordHash, userPasswordSalt))
                    {
                        return BadRequest("Wrong password");
                    }
                    token = CreateToken(user);
                    
                    break;
                }
            }
            if (!userExists)
            {
                return BadRequest("User not found.");
            }  

            return Ok(token);
        }

        private string CreateToken(HcmUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRoleId.ToString())
        };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection
                ("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
