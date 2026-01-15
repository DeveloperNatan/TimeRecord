using System.ComponentModel.DataAnnotations;
using TimeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Company;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController(BusinessService businessService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await businessService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyCreateDTO dto)
        {
            var user = await businessService.CreateCompanyAsync(dto);
            return Ok(user);
        }
    }
}