using System.ComponentModel.DataAnnotations;
using TimeRecord.Models;

namespace TimeRecord.Validation
{
    public static class EmployeeValidator
    {
        public static void Validate(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Nome) || employee.Nome == "string")
            {
                throw new ValidationException("Digite nome valido!");
            }

            if (string.IsNullOrWhiteSpace(employee.Cargo) || employee.Cargo == "string")
            {
                throw new ValidationException("Digite um cargo valido!");
            }

            if (string.IsNullOrWhiteSpace(employee.Email) || !employee.Email.Contains("@"))
            {
                throw new ValidationException("Email invlaido");
            }
            if (string.IsNullOrWhiteSpace(employee.Senha) || employee.Senha == "string")
            {
                throw new ValidationException("Senha invalida");
            }
        }
    }
}
