using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VirtualTeacher.Models.enums;

namespace VirtualTeacher.Models;

public class User
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8), MaxLength(64)]
    public string Password { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(32768)]
    public string? AvatarUrl { get; set; }

    [Required]
    public UserRole UserRole { get; set; }

    public IList<Course> Courses { get; set; } = null!;
    public IList<Lecture> Lectures { get; set; } = null!;
    public IList<Assignment> Assignments { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;

    // teachers only
    public IList<Course> CreatedCourses { get; set; } = null!;
    public IList<Lecture> CreatedLectures { get; set; } = null!;
    public IList<Assignment> CreatedAssignments { get; set; } = null!;
    public IList<Grade> GivenGrades { get; set; } = null!;
}