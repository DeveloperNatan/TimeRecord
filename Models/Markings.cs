using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeRecord.Models
{
    public class Marking
    {
        [Key]
        public int PontoId { get; set; }

        // [ForeignKey("Employee")]
        public int MatriculaId { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public string MarkingType { get; set; }

        // Navegação para o funcionário
        // public Employee Employee { get; set; }
    }
}
