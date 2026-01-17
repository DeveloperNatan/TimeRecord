using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
