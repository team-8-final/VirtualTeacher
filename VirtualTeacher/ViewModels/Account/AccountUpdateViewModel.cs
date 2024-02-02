using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.ViewModels.Account;

public class AccountUpdateViewModel
{
    public string Username { get; set; } = "";

    [Required(AllowEmptyStrings = false, ErrorMessage = "First name cannot be blank.")]
    [MinLength(2, ErrorMessage = "First name must be at least {1} characters.")]
    [MaxLength(20, ErrorMessage = "First name must be less than {1} characters.")]
    public string FirstName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Last name cannot be blank.")]
    [MinLength(2, ErrorMessage = "Last name must be at least {1} characters.")]
    [MaxLength(20, ErrorMessage = "Last name must be less than {1} characters.")]
    public string LastName { get; set; } = null!;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be blank.")]
    [EmailAddress(ErrorMessage = "Enter valid email address.")]
    public string Email { get; set; } = null!;

    public string AvatarUrl { get; set; } = "";
}