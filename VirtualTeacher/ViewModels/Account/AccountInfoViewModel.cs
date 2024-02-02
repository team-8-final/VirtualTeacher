using VirtualTeacher.Models;

namespace VirtualTeacher.ViewModels.Account;

public class AccountInfoViewModel
{
    public string Username { get; set; } = "";

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AvatarUrl { get; set; } = null!;

    public IList<Course> EnrolledCourses { get; set; } = null!;

    public IList<Course> CreatedCourses { get; set; } = null!;

}