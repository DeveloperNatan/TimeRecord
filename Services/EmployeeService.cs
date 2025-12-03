using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<EmployeeResponseDTO> Post(Employee employee)
        {
            EmployeeValidator.Validate(employee);
            employee.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);

            _appdbcontext.Employees.Add(employee);

            await _appdbcontext.SaveChangesAsync();
            return new EmployeeResponseDTO
            {
                MatriculaId = employee.MatriculaId,
                Nome = employee.Nome,
            };
        }

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

        public async Task<IEnumerable<Employee>> FindAll()
        {
            var UserAll = await _appdbcontext.Employees.ToListAsync();
            if (UserAll == null)
            {
                throw new Exception("Nenhum usuario encontrado!");
            }
            return UserAll;
        }

        public async Task<EmployeeResponseDTO> FindOne(int id)
        {
            var User = await _appdbcontext.Employees.FindAsync(id);
            if (User == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado!");
            }
            return new EmployeeResponseDTO
            {
                MatriculaId = User.MatriculaId,
                Nome = User.Nome,
                Cargo = User.Cargo,
                Email = User.Email,
            };
        }

        public async Task<EmployeeResponseDTO> Delete(int id)
        {
            var User = await _appdbcontext.Employees.FindAsync(id);
            if (User == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado!");
            }
            _appdbcontext.Remove(User);
            await _appdbcontext.SaveChangesAsync();
            return new EmployeeResponseDTO { MatriculaId = User.MatriculaId };
        }

        public async Task<EmployeeResponseDTO> Update(Employee employee, int id)
        {
            var user = await _appdbcontext
                .Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.MatriculaId == id);

            if (user == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado!");
            }
            EmployeeValidator.Validate(employee);
            user.Nome = employee.Nome;
            user.Cargo = employee.Cargo;
            user.Email = employee.Email;

            if (!string.IsNullOrWhiteSpace(employee.Senha))
            {
                user.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);
            }

            await _appdbcontext.SaveChangesAsync();
            return new EmployeeResponseDTO
            {
                MatriculaId = user.MatriculaId,
                Nome = user.Nome,
                Cargo = user.Cargo,
                Email = user.Email,
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

            if (!markings.Any())
            {
                throw new Exception("Nenhum registro encontrado!");
            }
            return markings;
        }
    }
}
