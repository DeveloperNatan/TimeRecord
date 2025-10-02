using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegistrarPonto.Data;
using RegistrarPonto.Models;

namespace RegistrarPonto.Services
{
    public class EmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async IEnumerable<Employee> Register()
        {
            var result = 
            await _app
        }
    }
}
