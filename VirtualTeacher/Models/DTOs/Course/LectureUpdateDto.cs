namespace VirtualTeacher.Models.DTOs.Course
{
    public class LectureUpdateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string VideoLink { get; set; } = null!;
        public string AssignmentLink { get; set; } = null!;
        public string CourseTitle { get; set; } = null!;
        public string TeacherUsername { get; set; } = null!;
        public string TeacherFirstName { get; set; } = null!;
        public string TeacherLastName { get; set; } = null!;
    }
}
