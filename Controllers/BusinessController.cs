using System.ComponentModel.DataAnnotations;
using TimeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
 [ApiController]
 [Route("api/[controller]")]
    public class BusinessController: ControllerBase
    {
        private readonly BusinessService _businessService;


        public BusinessController(BusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            try
            {
                var users = await _businessService.FindAll();
                return Ok(users);
            }
            catch (ValidationException error)
            {
                return BadRequest(error);
            }
        }
    }
}
