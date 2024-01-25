using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.Course;

public class CourseResponseDto
{
    public int Id { get; set; }

    [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
    public string? Title { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? StartingDate { get; set; }

    public string CourseTopic { get; set; } = null!;

    public bool? Published { get; set; }

    // todo fix Models.User path
    public IList<Models.User> EnrolledStudents { get; set; } = null!;
    public IList<Lecture> Lectures { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;

    // todo fix Models.User path
    public IList<Models.User> ActiveTeachers { get; set; } = null!;
}