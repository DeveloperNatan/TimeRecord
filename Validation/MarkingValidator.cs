using System.ComponentModel.DataAnnotations;
using TimeRecord.Models;

namespace TimeRecord.Validation
{
    public static class MarkingValidator
    {
        public static void Validate(Marking marking)
        {
            if (marking.MatriculaId <= 0)
            {
                throw new ValidationException("Employee ID not found in the system!");
            }
        }
    }
}
