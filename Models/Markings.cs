using System.ComponentModel.DataAnnotations;

namespace TimeRecord.Models
{
    public class Marking
    {
        [Key]
        public int PontoId { get; set; }
        
        public int MatriculaId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
