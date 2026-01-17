using System.Text.Json.Serialization;

namespace TimeRecord.DTO.Markings
{
    public class MarkingsCreateDTO
    {
      
        public int PontoId { get; set; }
        public int MatriculaId { get; set; }
       
        public DateTime Timestamp { get; set; }
    }
}

