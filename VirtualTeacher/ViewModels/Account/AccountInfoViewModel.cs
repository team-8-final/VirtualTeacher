using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.ViewModels.Account;

public class AccountInfoViewModel
{
    public string Username { get; set; } = "";

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public UserRole UserRole { get; set; }

    public IList<Course> EnrolledCourses { get; set; } = null!;

    public IList<Course> CreatedCourses { get; set; } = null!;

    public IList<Course> CompletedCourses { get; set; } = null!;

    public IList<Course> RatedCourses { get; set; } = null!;

    // TODO: implement when course comments are complete
    // public IList<Course> CourseComments { get; set; } = null!;
}