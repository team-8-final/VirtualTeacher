using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.Course;

public class CourseUpdateDto
{
    public int Id { get; set; }

    [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
    public string? Title { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? StartingDate { get; set; }

    public CourseTopic CourseTopic { get; set; }

    public bool? Published { get; set; }
}