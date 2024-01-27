using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Course;

public class LectureResponseDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string VideoLink { get; set; } = null!;
    public string AssignmentLink { get; set; } = null!;
    public string CourseTitle { get; set; } = null!;
    public string TeacherUsername { get; set; } = null!;

    public IList<string> WatchedBy { get; set; } = null!;
    public IList<string> Notes { get; set; } = null!;
    public IList<string> Comments { get; set; } = null!;
}