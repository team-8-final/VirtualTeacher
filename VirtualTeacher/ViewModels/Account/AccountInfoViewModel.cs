using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.ViewModels.Account;

public class AccountInfoViewModel
{
    public int Id { get; set; }

    public string Username { get; set; } = "";

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public UserRole UserRole { get; set; }

    public IList<Course> EnrolledCourses { get; set; } = new List<Course>();

    public IList<Course> CreatedCourses { get; set; } = new List<Course>();

    public IList<Course> CompletedCourses { get; set; } = new List<Course>();

    public IList<Course> RatedCourses { get; set; } = new List<Course>();

    public List<Comment> CourseComments { get; set; } = null!;
    public TeacherApplication? TeacherApplication { get; set; }
}