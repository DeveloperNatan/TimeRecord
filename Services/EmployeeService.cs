using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Data;
using TimeRecord.Validation;
using TimeRecord.DTO.Employee;
using TimeRecord.DTO.Markings;
using TimeRecord.Models;

namespace TimeRecord.Services
{
    public class EmployeeService(AppDbContext appDbContext)
    {
        public async Task<EmployeeResponseDto> CreateUserAsync(EmployeeCreateAndUpdateDto dataDto)
        {
            EmployeeValidator.Validate(dataDto);
            if (!EmailValidator.IsValidEmail(dataDto))
            {
                throw new ValidationException("Email invalid");
            }

            dataDto.Password = BCrypt.Net.BCrypt.HashPassword(dataDto.Password);

            var createdEmployee = new Employee()
            {
                Name = dataDto.Name,
                Role = dataDto.Role,
                Email = dataDto.Email,
                Password = dataDto.Password,
            };
            
            await appDbContext.Employees.AddAsync(createdEmployee);
            await appDbContext.SaveChangesAsync();

            var response = new EmployeeResponseDto()
            {
                RegistrationId = createdEmployee.RegistrationId,
                Name = createdEmployee.Name,
                Role = createdEmployee.Role,
                Email = createdEmployee.Email,
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
                RegistrationId = authenticatedEmployee.RegistrationId,
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
                RegistrationId = employee.RegistrationId,
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
                RegistrationId = employee.RegistrationId,
                Name = employee.Name,
                Role = employee.Name,
                Email = employee.Email,
            };
        }

        public async Task<EmployeeMessageDto> DeleteUserAsync(int id)
        {
            var deletedEmployee = await appDbContext.Employees.FindAsync(id);
            if (deletedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            appDbContext.Remove(deletedEmployee);
            await appDbContext.SaveChangesAsync();
            
            var response = new EmployeeMessageDto()
            {
                Messsage = $"User {deletedEmployee.Name} has been deleted successfully!"
            };
            return response;
        }

        public async Task<EmployeeResponseDto> UpdateUserAsync(EmployeeCreateAndUpdateDto dataDto, int id)
        {
            var updatedEmployee = await appDbContext
                .Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.RegistrationId == id);

            if (updatedEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            EmployeeValidator.Validate(dataDto);
            updatedEmployee.Name = dataDto.Name;
            updatedEmployee.Role = dataDto.Role;
            updatedEmployee.Email = dataDto.Email;

            if (!string.IsNullOrWhiteSpace(dataDto.Password))
            {
                updatedEmployee.Password = BCrypt.Net.BCrypt.HashPassword(dataDto.Password);
            }

            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDto
            {
                RegistrationId = updatedEmployee.RegistrationId,
                Name = updatedEmployee.Name,
                Role = updatedEmployee.Role,
                Email = updatedEmployee.Email,
            };
        }

        public async Task<IEnumerable<MarkingsResponseDto>> GetMarkingUserAsync(int id)
        {
            var markingsEmployee = await appDbContext.Employees.FindAsync(id);
            if (markingsEmployee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            var markings = await appDbContext
                .Markings.Where(m => m.RegistrationId == id)
                .ToListAsync();

            if (markings.Count == 0)
            {
                throw new KeyNotFoundException("No time markings found for this employee!");
            }

            var response = markings.Select(employeeMarking => new MarkingsResponseDto()
            {
               RegistrationId = employeeMarking.RegistrationId,
               Timestamp = employeeMarking.Timestamp.ToString("dd/MM/yyyy HH:mm"),
            });
            
            return response;
        }
    }
}