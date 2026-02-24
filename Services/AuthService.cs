using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TimeRecord.Data;
using TimeRecord.DTO.Login;
using TimeRecord.Models;

namespace TimeRecord.Services;

public class AuthService(AppDbContext appDbContext)
{
    public async Task<Token> GenerateToken(string email, string password)
    {
        var userDb = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (userDb == null)
        {
            throw new KeyNotFoundException("User not found!");
        }

        bool VerifyPassword(string passwordEntered)
        {
            return BCrypt.Net.BCrypt.Verify(passwordEntered,  userDb.PasswordHash);
        }

        if (!VerifyPassword(password))
        {
            throw new UnauthorizedAccessException("Password incorrect!");
        }

        var user = new User
        {
            Id = userDb.Id,
            Email = userDb.Email,
            PasswordHash = userDb.PasswordHash,
            Roles = new[] { "developer" }
        };

        var (token, expiresUtc) = GetToken(user);
        
        return new Token()
        {
            AcecessToken = token,
            TokenType =  "Bearer",
            ExpiresIn = (int)(expiresUtc - DateTime.UtcNow).TotalSeconds,
        };
    }
    private (string Token, DateTime ExpiresUtc) GetToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes(JwtConfiguration.PrivateKey);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddHours(12);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = credentials,
            Expires = expires,
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return (handler.WriteToken(token), expires);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        var ci = new ClaimsIdentity("token");
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Email, user.Email));

        return ci;
    }


    public async Task<string> CreateUserAsync(LoginDto dataDto)
    {
        var existingEmail = await appDbContext.Users.AnyAsync(e => e.Email == dataDto.Email);

        if (existingEmail)
        {
            throw new ValidationException("This Email can't be used");
        }

        dataDto.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dataDto.PasswordHash);

        var createdEmail = new User()
        {
            Email = dataDto.Email,
            PasswordHash = dataDto.PasswordHash,
        };

        await appDbContext.Users.AddAsync(createdEmail);
        await appDbContext.SaveChangesAsync();


        var response = "User created!";
        return response;
    }
}