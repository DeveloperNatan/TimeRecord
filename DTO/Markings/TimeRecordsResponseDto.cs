namespace TimeRecord.DTO.Markings
{
    public class TimeRecordsResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}