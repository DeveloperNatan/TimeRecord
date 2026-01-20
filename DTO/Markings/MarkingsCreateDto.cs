using System.Text.Json.Serialization;

namespace TimeRecord.DTO.Markings
{
    public class MarkingsCreateDto
    {
      
        public int PontoId { get; set; }
        public int RegistrationId { get; set; }
       
        public string Timestamp { get; set; }
    }
}

