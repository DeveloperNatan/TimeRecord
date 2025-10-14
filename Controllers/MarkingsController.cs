using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class markingsController : ControllerBase
    {
        private readonly MarkingsService _markingsService;

        public markingsController(MarkingsService markingsservice)
        {
            _markingsService = markingsservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Marking markings)
        {
            try
            {
                var CreateMarking = await _markingsService.Post(markings);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var FindMarking = await _markingsService.Find();
            return Ok(FindMarking);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var FindOneMarking = await _markingsService.FindOne(id);
            return Ok(FindOneMarking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var DeleteMarking = await _markingsService.DeleteOne(id);
            return Ok(DeleteMarking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Marking marking, int id)
        {
            var UpdateMarking = await _markingsService.UpdateOne(marking, id);
            return Ok(UpdateMarking);
        }
    }
}
