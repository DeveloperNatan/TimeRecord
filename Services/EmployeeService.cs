using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Employee>> FindAll()
        {
            var result = await _appdbcontext.Employees.ToListAsync();
            return result;
        }

        public async Task<Employee> FindOne(int id)
        {
            var result = await _appdbcontext.Employees.FindAsync(id);
            return result;
        }

        public async Task<Employee> Delete(int id)
        {
            var user = await _appdbcontext.Employees.FindAsync(id);
            _appdbcontext.Remove(user);
            await _appdbcontext.SaveChangesAsync();
            return user;
        }
    }
}
