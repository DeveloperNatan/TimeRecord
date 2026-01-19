using System.ComponentModel.DataAnnotations;
using TimeRecord.DTO.Employee;

namespace TimeRecord.Validation
{
    public static class EmployeeValidator
    {
        public static void Validate(EmployeeCreateDto employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name) || employee.Name == "string")
            {
                throw new ValidationException("Enter a name valid!");
            }

            if (string.IsNullOrWhiteSpace(employee.Role) || employee.Role == "string")
            {
                throw new ValidationException("Enter a Role valid!");
            }

  
            if (string.IsNullOrWhiteSpace(employee.Password) || employee.Password == "string")
            {
                throw new ValidationException("Enter password valid!");
            }
        }
    }
}
