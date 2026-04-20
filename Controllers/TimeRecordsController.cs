using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Markings;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeRecordsController(TimeRecordsService timeRecordsService) : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TimeRecordsCreateDto createRequestDto)
        {
            var createdMarking = await timeRecordsService.CreateMarkingAsync(createRequestDto);
            return Ok(createdMarking);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var markings = await timeRecordsService.GetAllMarkingsAsync();
            return Ok(markings);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var marking = await timeRecordsService.GetMarkingsAsync(id);
            return Ok(marking);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedMarking = await timeRecordsService.DeleteMarkingAsync(id);
            return Ok(deletedMarking);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var updatedMarking = await timeRecordsService.UpdateMarkingAsync(id);
            return Ok(updatedMarking);
        }
    }
}