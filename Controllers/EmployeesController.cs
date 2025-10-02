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
            var CreateUserService = await _employeeservice.Register(employee);
            return Ok(CreateUserService);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeService>>> FindAllUser(Employee employee)
        {
            var FindUserService = await _employeeservice.Find(employee);
        }
    }
}
