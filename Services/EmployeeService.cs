using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.Models;
using TimeRecord.Validation;

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
            EmployeeValidator.Validate(employee);
            employee.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);

            _appdbcontext.Employees.Add(employee);

            await _appdbcontext.SaveChangesAsync();
            return employee;
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

        // finalizado, regras de negocio ok e DTO ok
        public async Task<EmployeeResponseDTO> Authenticate(string email, string senha)
        {
            var user = await _appdbcontext.Employees.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado!");
            }

            bool VerifyPassword(string SenhaDigitada)
            {
                return BCrypt.Net.BCrypt.Verify(SenhaDigitada, user.Senha);
            }

            if (!VerifyPassword(senha))
            {
                throw new UnauthorizedAccessException("Senha incorreta!");
            }
            return new EmployeeResponseDTO
            {
                MatriculaId = user.MatriculaId,
                Nome = user.Nome,
                Cargo = user.Cargo,
            };
        }

        public async Task<IEnumerable<Marking>> FindMarkingsUser(int id)
        {
            var user = await _appdbcontext.Employees.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado!");
            }
            var markings = await _appdbcontext
                .Markings.Where(m => m.MatriculaId == id)
                .ToListAsync();

            return markings;
        }
    }
}
