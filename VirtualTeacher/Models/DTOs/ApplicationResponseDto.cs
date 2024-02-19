namespace VirtualTeacher.Models.DTOs
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public string Teacher { get; set; } = null!;
        public int TeacherId { get; set; }
        public string Course { get; set; } = null!;
        public int CourseId { get; set; }
    }
}
