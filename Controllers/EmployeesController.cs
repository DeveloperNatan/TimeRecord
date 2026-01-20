using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Login;
using TimeRecord.DTO.Employee;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController(EmployeeService employeeService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(EmployeeCreateDto createRequestDto)
        {
            var createdEmployee = await employeeService.CreateUserAsync(createRequestDto);
            return Ok(createdEmployee);
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateLogin(LoginDto requestLoginDto)
        {
            var connectedEmployee = await employeeService.AuthenticateUser(requestLoginDto.Email, requestLoginDto.Password);
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
            return Ok(deletedEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(EmployeeCreateDto createRequestDto, int id)
        {
            var editedEmployee = await employeeService.UpdateUserAsync(createRequestDto, id);
            return Ok(editedEmployee);
        }

        [HttpGet("{id}/markings")]
        public async Task<IActionResult> GetMarkingAsync(int id)
        {
            var markingsEmployee = await employeeService.GetMarkingUserAsync(id);
            return Ok(markingsEmployee);
        }
    }
}