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
                await _markingsService.Post(req);
                return Ok("Sucesso ao criar marcação.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao tentar criar registro.");
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
            catch (Exception)
            {
                return BadRequest("Erro ao encontrar marcações.");
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
            catch (Exception)
            {
                return BadRequest("Erro ao encontrar marcação.");
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
            catch (Exception)
            {
                return BadRequest("Erro ao excluir marcação. ");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Marking marking, int id)
        {
            try
            {
                await _markingsService.UpdateOne(marking, id);
                return Ok("Sucesso ao editar marcação.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao ediatr marcação.");
            }
        }
    }
}
