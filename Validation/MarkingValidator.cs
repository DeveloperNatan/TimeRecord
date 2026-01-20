using System.ComponentModel.DataAnnotations;
using TimeRecord.DTO.Markings;
using TimeRecord.Models;

namespace TimeRecord.Validation
{
    public static class MarkingValidator
    {
        public static void Validate(Marking marking)
        {
            if (marking.RegistrationId <= 0)
            {
                throw new ValidationException("Employee ID not found in the system!");
            }
        }
    }
}
