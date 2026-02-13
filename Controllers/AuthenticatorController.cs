using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AuthenticatorController(AuthService authService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var user = new User
            {
                Id = 1,
                Email = "natan@gmail.com",
                PasswordHash = "1234",
                Roles = new[] { "developer" }
            };

            var token = authService.Create(user);
            return Ok(token);
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("logged-in");
        }
    }
}