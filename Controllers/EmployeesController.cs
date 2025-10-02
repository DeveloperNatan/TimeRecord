using Microsoft.AspNetCore.Mvc;
using RegistrarPonto.Data;
using RegistrarPonto.Models;

namespace RegistrarPonto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public EmployeesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public Task<IActionResult> CreateUser(Employee employee)
        {
            var result = _appDbContext.Employees.Add(employee);

            return result;
        }
    }
}
