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

        public async Task<Employee> Post(Employee employee)
        {
            var CreateEmployee = _appdbcontext.Employees.Add(employee);
            await _appdbcontext.SaveChangesAsync();
            return CreateEmployee.Entity;
        }

        public async Task<IEnumerable<Employee>> FindAll()
        {
            var UserAll = await _appdbcontext.Employees.ToListAsync();
            return UserAll;
        }

        public async Task<Employee> FindOne(int id)
        {
            var User = await _appdbcontext.Employees.FindAsync(id);
            return User;
        }

        public async Task<Employee> Delete(int id)
        {
            var UserDelete = await _appdbcontext.Employees.FindAsync(id);
            _appdbcontext.Remove(UserDelete);
            await _appdbcontext.SaveChangesAsync();
            return UserDelete;
        }

        public async Task<Employee> Update(Employee employee, int id)
        {
            var UserUpdate = await _appdbcontext.Employees.FindAsync(id);
            Console.WriteLine(UserUpdate);
            UserUpdate.Nome = employee.Nome;
            UserUpdate.Cargo = employee.Cargo;
            UserUpdate.Email = employee.Email;

            _appdbcontext.Update(UserUpdate);
            await _appdbcontext.SaveChangesAsync();
            return UserUpdate;
        }

        public async Task<Employee> Update(int id, [FromBody] Employee employeCurrent)
        {
            var CurrentUser = await _appdbcontext.Employees.FindAsync(id);

            _appdbcontext.Entry(CurrentUser).CurrentValues.SetValues(employeCurrent);

            await _appdbcontext.SaveChangesAsync();
            return CurrentUser;
        }
    }
}
