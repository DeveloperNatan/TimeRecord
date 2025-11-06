using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Controllers;
using TimeRecord.Data;
using TimeRecord.Models;

namespace TimeRecord.Services
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
            employee.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);
            _appdbcontext.Employees.Add(employee);
            try
            {
                await _appdbcontext.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> FindAll()
        {
            try
            {
                var UserAll = await _appdbcontext.Employees.ToListAsync();
                return UserAll;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> FindOne(int id)
        {
            var User = await _appdbcontext.Employees.FindAsync(id);
            try
            {
                return User;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> Delete(int id)
        {
            var UserDelete = await _appdbcontext.Employees.FindAsync(id);
            try
            {
                _appdbcontext.Remove(UserDelete);
                await _appdbcontext.SaveChangesAsync();
                return UserDelete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> Update(Employee employee, int id)
        {
            var UserUpdate = await _appdbcontext.Employees.FindAsync(id);
            try
            {
                UserUpdate.Nome = employee.Nome;
                UserUpdate.Cargo = employee.Cargo;
                UserUpdate.Email = employee.Email;
                _appdbcontext.Update(UserUpdate);
                await _appdbcontext.SaveChangesAsync();
                return UserUpdate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> FindEmail(string email)
        {
            var user = await _appdbcontext.Employees.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public bool VerifyPassword(Employee user, string senha)
        {
            return BCrypt.Net.BCrypt.Verify(senha, user.Senha);
        }
    }
}
