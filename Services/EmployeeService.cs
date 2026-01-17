using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Data;
using TimeRecord.Models;
using TimeRecord.Validation;

namespace TimeRecord.Services
{
    public class EmployeeService(AppDbContext appDbContext)
    {
        public async Task<EmployeeResponseDTO> CreateUserAsync(Employee employee)
        {
            EmployeeValidator.Validate(employee);
            if (!EmailValidator.IsValidEmail(employee))
            {
                throw new ValidationException("Email invalid");
            }

            employee.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);

            appDbContext.Employees.Add(employee);

            await appDbContext.SaveChangesAsync();
            var response = new EmployeeResponseDTO
            {
                MatriculaId = employee.MatriculaId,
                Nome = employee.Nome,
            };
            return response;
        }

        public async Task<EmployeeResponseDTO> AuthenticateUser(string email, string senha)
        {
            var authenticatedEmployee = await appDbContext.Employees.FirstOrDefaultAsync(u => u.Email == email);
            if (authenticatedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            bool VerifyPassword(string passwordEntered)
            {
                return BCrypt.Net.BCrypt.Verify(passwordEntered, authenticatedEmployee.Senha);
            }

            if (!VerifyPassword(senha))
            {
                throw new UnauthorizedAccessException("Password incorrect!");
            }
            
            var response = new EmployeeResponseDTO
            {
                MatriculaId = authenticatedEmployee.MatriculaId,
                Nome = authenticatedEmployee.Nome,
                Cargo = authenticatedEmployee.Cargo,
            };
            return response;
        }

        public async Task<IEnumerable<Employee>> GetAllUsersAsync()
        {
            var employees = await appDbContext.Employees.ToListAsync();
            if (!employees.Any())
            {
                throw new KeyNotFoundException("There are no users in the system!");
            }

            return employees;
        }

        public async Task<EmployeeResponseDTO> GetUserAsync(int id)
        {
            var employee = await appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            return new EmployeeResponseDTO
            {
                MatriculaId = employee.MatriculaId,
                Nome = employee.Nome,
                Cargo = employee.Cargo,
                Email = employee.Email,
            };
        }

        public async Task<EmployeeResponseDTO> DeleteUserAsync(int id)
        {
            var deletedEmployee = await appDbContext.Employees.FindAsync(id);
            if (deletedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            appDbContext.Remove(deletedEmployee);
            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDTO { MatriculaId = deletedEmployee.MatriculaId };
        }

        public async Task<EmployeeResponseDTO> UpdateUserAsync(Employee employee, int id)
        {
            var updatedEmployee = await appDbContext
                .Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.MatriculaId == id);

            if (updatedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            EmployeeValidator.Validate(employee);
            updatedEmployee.Nome = employee.Nome;
            updatedEmployee.Cargo = employee.Cargo;
            updatedEmployee.Email = employee.Email;

            if (!string.IsNullOrWhiteSpace(employee.Senha))
            {
                updatedEmployee.Senha = BCrypt.Net.BCrypt.HashPassword(employee.Senha);
            }

            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDTO
            {
                MatriculaId = updatedEmployee.MatriculaId,
                Nome = updatedEmployee.Nome,
                Cargo = updatedEmployee.Cargo,
                Email = updatedEmployee.Email,
            };
        }

        public async Task<IEnumerable<Marking>> GetMarkingUserAsync(int id)
        {
            var markingsEmployee = await appDbContext.Employees.FindAsync(id);
            if (markingsEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            var markings = await appDbContext
                .Markings.Where(m => m.MatriculaId == id)
                .ToListAsync();

            if (!markings.Any())
            {
                throw new Exception("No time markings found for this employee!");
            }

            return markings;
        }
    }
}