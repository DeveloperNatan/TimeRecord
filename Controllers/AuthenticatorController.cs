using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Data;
using TimeRecord.DTO.Login;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AuthenticatorController(AuthService authService, AppDbContext appDbContext) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ValidateUserAsync(LoginDto requestLoginDto)
        {
            var validatedUser = await authService.GenerateToken(requestLoginDto.Email, requestLoginDto.PasswordHash);
            return Ok(validatedUser);
        }
        
        [HttpPost("Login")]
        public async Task<IActionResult> CreateAsync(LoginDto requestLoginDto)
        {
            var userCreated = await authService.CreateUserAsync(requestLoginDto);
            return Ok(userCreated);
        }


        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                isAuth = User.Identity?.IsAuthenticated
            });
        }
    }
}