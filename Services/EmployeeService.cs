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
        public async Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeCreateAndUpdateDto dataDto)
        {
            EmployeeValidator.Validate(dataDto);
            var exisingEmployee = await appDbContext.Employees.AnyAsync(e => e.Name == dataDto.Name);
     
            
            if (exisingEmployee)
            {
                throw new ValidationException("This name already exists, try another");
            }
      
      

            var createdEmployee = new Employee()
            {
                Name = dataDto.Name,
                Job = dataDto.Job,
                UserId = 1,
            };
            
            await appDbContext.Employees.AddAsync(createdEmployee);
            await appDbContext.SaveChangesAsync();

            var response = new EmployeeResponseDto()
            {
                RegistrationId = createdEmployee.RegistrationId,
                Name = createdEmployee.Name,
          
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
                Job = employee.Name,
       
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
                Job = employee.Name,
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
   
  
            await appDbContext.SaveChangesAsync();
            return new EmployeeResponseDto
            {
                RegistrationId = updatedEmployee.RegistrationId,
                Name = updatedEmployee.Name,
     
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