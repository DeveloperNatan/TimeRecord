using System.ComponentModel.DataAnnotations;
using TimeRecord.DTO.Employee;

namespace TimeRecord.Validation
{
    public static class EmployeeValidator
    {
        public static void Validate(EmployeeCreateAndUpdateDto employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                throw new ValidationException("Enter a name valid!");
            }

            if (string.IsNullOrWhiteSpace(employee.Role))
            {
                throw new ValidationException("Enter a Role valid!");
            }

            if (string.IsNullOrWhiteSpace(employee.Password))
            {
                throw new ValidationException("Enter password valid!");
            }
        }
    }
}