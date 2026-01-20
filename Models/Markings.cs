using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TimeRecord.Models
{
    public class Marking
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PontoId { get; set; }

        public int RegistrationId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}