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
                throw new ValidationException("Matricula invalida!");
            }
            if (string.IsNullOrWhiteSpace(marking.MarkingType) || marking.MarkingType == "")
            {
                throw new ValidationException("Tipo de marcação invalido!");
            }
        }
    }
}
