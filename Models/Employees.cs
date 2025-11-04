using System.ComponentModel.DataAnnotations;

namespace TimeRecord.Models
{
    public class Employee
    {
        [Key]
        public int MatriculaId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Cargo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Relacionamento 1:N → um Employee tem várias marcações
        // public ICollection<Marking> Markings { get; set; } = new List<Marking>();
    }
}
