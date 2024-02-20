using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
    public string Title { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? StartingDate { get; set; }

    public CourseTopic CourseTopic { get; set; }

    [MaxLength(8192)] public string VideoLink { get; set; } = "https://www.youtube.com/embed/gwXpWWxZeJo";

    public bool Published { get; set; }

    public bool IsDeleted { get; set; }

    public IList<User> EnrolledStudents { get; set; } = null!;
    public IList<Lecture> Lectures { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;
    public IList<User> ActiveTeachers { get; set; } = null!;
}