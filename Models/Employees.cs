using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeRecord.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Senha { get; set; }
    }
}
