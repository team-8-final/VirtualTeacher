namespace VirtualTeacher.Models.DTOs.Account;

public class UserPasswordDto
{
    public string CurrentPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;
}