namespace VirtualTeacher.Models.DTOs.Course
{
    public class CommentResponseDto
    {
        public int Id { get; set; }
        public string AuthorUsername { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
