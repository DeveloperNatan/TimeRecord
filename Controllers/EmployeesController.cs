using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Models;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(EmployeeService employeeService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Employee employee)
        {
            var createdEmployee = await employeeService.CreateUserAsync(employee);
            return Ok(createdEmployee);
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateLogin(LoginDTO login)
        {
            var connectedEmployee = await employeeService.AuthenticateUser(login.Email, login.Senha);
            return Ok(connectedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var employees = await employeeService.GetAllUsersAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var employee = await employeeService.GetUserAsync(id);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedEmployee = await employeeService.DeleteUserAsync(id);
            return Ok(new { message = $"User {deletedEmployee.MatriculaId} deleted!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Employee employee, int id)
        {
            var editedEmployee = await employeeService.UpdateUserAsync(employee, id);
            return Ok(new { message = $"User {editedEmployee.MatriculaId} edited successfully." });
        }

        [HttpGet("{id}/markings")]
        public async Task<IActionResult> GetMarkingAsync(int id)
        {
            var markingsEmployee = await employeeService.GetMarkingUserAsync(id);
            return Ok(markingsEmployee);
        }
    }
}