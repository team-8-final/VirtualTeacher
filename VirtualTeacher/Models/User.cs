using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models;

public class User
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    [MinLength(4), MaxLength(20)]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(254)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8), MaxLength(128)]
    public string Password { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string LastName { get; set; } = null!;

    [Required]
    public UserRole UserRole { get; set; }
    public bool IsDeleted { get; set; } = false;

    public IList<Course> EnrolledCourses { get; set; } = null!;
    public IList<Lecture> WatchedLectures { get; set; } = null!;
    public IList<Submission> Submissions { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;
    public IList<Note> Notes { get; set; } = null!;
    public IList<Comment> LectureComments { get; set; } = null!;

    public int? TeacherApplicationdId { get; set; }
    public TeacherApplication? TeacherApplication { get; set; }


    // teachers only
    public IList<Course> CreatedCourses { get; set; } = null!;
    public IList<Lecture> CreatedLectures { get; set; } = null!;
    public IList<Submission> CreatedAssignments { get; set; } = null!;
}