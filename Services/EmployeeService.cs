using RegistrarPonto.Data;
using RegistrarPonto.Models;

namespace RegistrarPonto.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _appdbcontext;

        public EmployeeService(AppDbContext appdbcontext)
        {
            _appdbcontext = appdbcontext;
        }

        public async Task<Employee> Register(Employee employee)
        {
            var result = _appdbcontext.Employees.Add(employee);
            await _appdbcontext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
