using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Markings;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkingsController(MarkingsService markingsService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MarkingsCreateDTO dto)
        {
            var createdMarking = await markingsService.CreateMarkingAsync(dto);
            return Ok(createdMarking);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var markings = await markingsService.GetAllMarkingsAsync();
            return Ok(markings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var marking = await markingsService.GetMarkingsAsync(id);
            return Ok(marking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedMarking = await markingsService.DeleteMarkingAsync(id);
            return Ok(deletedMarking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var updatedMarking = await markingsService.UpdateMarkingAsync(id);
            return Ok(updatedMarking);
        }
    }
}