using System.ComponentModel.DataAnnotations;
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
        public async Task<IActionResult> CreateMarking([FromBody] Marking req)
        {
            try
            {
                var marking = await _markingsService.Post(req);
                return Ok(marking);
            }
            catch (ValidationException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                var Markings = await _markingsService.Find();
                return Ok(Markings);
            }
            catch (ValidationException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                var OneMarking = await _markingsService.FindOne(id);
                return Ok(OneMarking);
            }
            catch (ValidationException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _markingsService.DeleteOne(id);
                return Ok("Sucesso ao excluir marcação.");
            }
            catch (ValidationException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Marking marking, int id)
        {
            try
            {
                var time = await _markingsService.UpdateOne(marking, id);
                return Ok(time);
            }
            catch (ValidationException error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
