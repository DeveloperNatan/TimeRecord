using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Data;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AuthenticatorController(AuthService authService, AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<Token> GenerateToken(string email, string password)
        {
            var UserDb = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (UserDb == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            if (password != UserDb.PasswordHash)
            {
                throw new KeyNotFoundException("Password was not correct!");
            }
            
            var user = new User
            {
                Id = UserDb.Id,
                Email = UserDb.Email,
                PasswordHash = UserDb.PasswordHash,
                Roles = new[] { "developer" }
            };

            var token = authService.GetUser(user);
            return new Token()
            {
                TokenJwt = token
            };
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("logged-in");
        }
        
        
     
    }
}