using Microsoft.AspNetCore.Mvc;
using RegistrarPonto.Data;
using RegistrarPonto.Models;
using RegistrarPonto.Services;

namespace RegistrarPonto.Controllers
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
            var CreateUserService = await _employeeservice.Post(employee);
            return Ok(CreateUserService);
        }

        [HttpGet]
        public async Task<IActionResult> FindAllUser()
        {
            var FindUserService = await _employeeservice.FindAll();
            return Ok(FindUserService);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindOneUser(int id)
        {
            var FindOneUserService = await _employeeservice.FindOne(id);
            return Ok(FindOneUserService);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var DeleteOneUserService = await _employeeservice.Delete(id);
            return Ok(DeleteOneUserService);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Employee employee)
        {
            var UpdateOneUserService = await _employeeservice.Update(id, employee);
            return Ok(UpdateOneUserService);
        }
    }
}
