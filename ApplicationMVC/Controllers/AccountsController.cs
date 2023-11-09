using ApplicationMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Composition.Convention;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApplicationMVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration; 
        private readonly Uri baseAddress = new("https://localhost:7241/api/");
        private readonly HttpClient _client;

        public AccountsController(IConfiguration configuration)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login) 
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format("Login?username={0}&password={1}", login.Username, login.Password));
            if (response.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(token))
                {
                    ClaimsPrincipal principal = GetPrincipal(token);
                    if (principal != null)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error");
            }
            return View();
        }
        private ClaimsPrincipal GetPrincipal(string token)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection
                ("AppSecret:Token").Value));
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);



            if (jwtToken == null)
            {
                return null;
            }
            var parameter = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = key

            };
            SecurityToken securityToken;
            ClaimsPrincipal principal = handler.ValidateToken(token, parameter, out securityToken);

            return principal;

        }


    }
}
