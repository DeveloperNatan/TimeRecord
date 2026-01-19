using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Data;
using TimeRecord.Models;
using TimeRecord.Validation;
using TimeRecord.DTO.Employee;

namespace TimeRecord.Services
{
    public class EmployeeService(AppDbContext appDbContext)
    {
        public async Task<EmployeeResponseDto> CreateUserAsync(EmployeeCreateDto dto)
        {
            EmployeeValidator.Validate(dto);
            if (!EmailValidator.IsValidEmail(dto))
            {
                throw new ValidationException("Email invalid");
            }

            dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var createdEmployee = new Employee()
            {
                Name = dto.Name,
                Role = dto.Role,
                Email = dto.Email,
                Password = dto.Password,
            };
            await appDbContext.Employees.AddAsync(createdEmployee);
            await appDbContext.SaveChangesAsync();

            var response = new EmployeeResponseDto()
            {
                MatriculaId = createdEmployee.MatriculaId,
                Name = createdEmployee.Name,
            };
            return response;
        }

        public async Task<EmployeeResponseDto> AuthenticateUser(string email, string password)
        {
            var authenticatedEmployee = await appDbContext.Employees.FirstOrDefaultAsync(u => u.Email == email);
            if (authenticatedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            bool VerifyPassword(string passwordEntered)
            {
                return BCrypt.Net.BCrypt.Verify(passwordEntered, authenticatedEmployee.Password);
            }

            if (!VerifyPassword(password))
            {
                throw new UnauthorizedAccessException("Password incorrect!");
            }

            var response = new EmployeeResponseDto
            {
                MatriculaId = authenticatedEmployee.MatriculaId,
                Name = authenticatedEmployee.Name,
                Role = authenticatedEmployee.Role,
            };
            return response;
        }

        public async Task<IEnumerable<EmployeeResponseDto>> GetAllUsersAsync()
        {
            var employees = await appDbContext.Employees.ToListAsync();
            if (!employees.Any())
            {
                throw new KeyNotFoundException("There are no users in the system!");
            }

            var response = employees.Select(employee => new EmployeeResponseDto()
            {
                MatriculaId = employee.MatriculaId,
                Name = employee.Name,
                Role = employee.Name,
                Email = employee.Email,
            });
            return response;
        }

        public async Task<EmployeeResponseDto> GetUserAsync(int id)
        {
            var employee = await appDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            return new EmployeeResponseDto
            {
                MatriculaId = employee.MatriculaId,
                Name = employee.Name,
                Role = employee.Name,
                Email = employee.Email,
            };
        }

        public async Task<EmployeeResponseDto> DeleteUserAsync(int id)
        {
            var deletedEmployee = await appDbContext.Employees.FindAsync(id);
            if (deletedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            appDbContext.Remove(deletedEmployee);
            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDto { MatriculaId = deletedEmployee.MatriculaId };
        }

        public async Task<EmployeeResponseDto> UpdateUserAsync(EmployeeCreateDto employee, int id)
        {
            var updatedEmployee = await appDbContext
                .Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.MatriculaId == id);

            if (updatedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            EmployeeValidator.Validate(employee);
            updatedEmployee.Name = employee.Name;
            updatedEmployee.Role = employee.Role;
            updatedEmployee.Email = employee.Email;

            if (!string.IsNullOrWhiteSpace(employee.Password))
            {
                updatedEmployee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
            }

            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDto
            {
                MatriculaId = updatedEmployee.MatriculaId,
                Name = updatedEmployee.Name,
                Role = updatedEmployee.Role,
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