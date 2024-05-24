using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projekt_Avancerad.NET.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projekt_Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationController(IUserService userService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel model)
        {

            var token = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }


        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Role))
            {
                return BadRequest("Invalid user registration");
            }

            if(!await _roleManager.RoleExistsAsync(model.Role))
            {
                return BadRequest("Role does not exist");
            }

            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded) 
            {
                return BadRequest(result.Errors);            
            }

            await _userManager.AddToRoleAsync(user, model.Role);
            return Ok(new { Message = "User Registered" });
        }


        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}
