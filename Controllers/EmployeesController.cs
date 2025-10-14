using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class employeesController : ControllerBase
    {
        private readonly EmployeeService _employeeservice;

        public employeesController(EmployeeService employeeservice)
        {
            _employeeservice = employeeservice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(Employee employee)
        {
            try
            {
                await _employeeservice.Post(employee);
                return Ok("Usuario criado com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao criar usuario.");
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
                return BadRequest("Erro ao buscar usuarios.");
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
            catch (Exception)
            {
                return BadRequest("Erro ao buscar usuario.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _employeeservice.Delete(id);
                return Ok("Usuario excluido com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao excluir usuario.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Employee employee, int id)
        {
            try
            {
                await _employeeservice.Update(employee, id);
                return Ok("Usuario editado com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest("Erro ao editar usuario.");
            }
        }
    }
}
