using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkingsController(MarkingsService markingsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMarking([FromBody] Marking req)
        {
            var marking = await markingsService.Post(req);
            return Ok(marking);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var marking = await markingsService.Find();
            return Ok(marking);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            var onemarking = await markingsService.FindOne(id);
            return Ok(onemarking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await markingsService.DeleteOne(id);
            return Ok("Marking edited successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Marking marking, int id)
        {
            var markingtime = await markingsService.UpdateOne(marking, id);
            return Ok(markingtime);
        }
    }
}