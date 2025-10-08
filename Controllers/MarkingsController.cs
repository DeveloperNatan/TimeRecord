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
            var CreateMarking = await _markingsService.Post(markings);
            return Ok(CreateMarking);
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
            var DeleteMarking = await _markingsService.Delete(id);
            return Ok(DeleteMarking);
        }
    }
}
