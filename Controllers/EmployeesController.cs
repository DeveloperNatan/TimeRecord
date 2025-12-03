using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeservice;

        public EmployeesController(EmployeeService employeeservice)
        {
            _employeeservice = employeeservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Employee employee)
        {
            try
            {
                var user = await _employeeservice.Post(employee);
                return Ok(user);
            }
            catch (ValidationException error)
            {
                return BadRequest(new { message = error.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateLogin(LoginDTO login)
        {
            try
            {
                var user = await _employeeservice.Authenticate(login.Email, login.Senha);
                return Ok(user);
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new { message = error.Message });
            }
            catch (UnauthorizedAccessException error)
            {
                return BadRequest(new { message = error.Message });
            }
            catch (Exception)
            {
                return BadRequest(
                    new { message = "Houve algum erro ao processar login, tente novamente!" }
                );
            }
        }

        [HttpGet]
        public async Task<IActionResult> FindAllUser()
        {
            try
            {
                var users = await _employeeservice.FindAll();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro interno ao buscar dados!" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindOneUser(int id)
        {
            try
            {
                var user = await _employeeservice.FindOne(id);
                return Ok(user);
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new { message = error.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro interno ao buscar usuario!" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _employeeservice.Delete(id);
                return Ok(new { message = $"Usuario {user.MatriculaId} deletado!" });
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new { message = error.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao excluir usuario." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromBody] Employee employee,
            [FromRoute] int id
        )
        {
            try
            {
                await _employeeservice.Update(employee, id);
                return Ok(new { message = "Usuario editado com sucesso." });
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new { message = error.Message });
            }
            catch (ValidationException error)
            {
                return BadRequest(new { message = error.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao editar usuario." });
            }
        }

        [HttpGet("{id}/markings")]
        public async Task<IActionResult> MarkingsUser(int id)
        {
            try
            {
                var Markings = await _employeeservice.FindMarkingsUser(id);
                return Ok(Markings);
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(new { message = error.Message });
            }
            catch (Exception error)
            {
                return BadRequest(new { message = error.Message });
            }
        }
    }
}
