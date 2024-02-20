namespace VirtualTeacher.Models.DTOs
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public string Student { get; set; } = null!;
        public int StudentId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
