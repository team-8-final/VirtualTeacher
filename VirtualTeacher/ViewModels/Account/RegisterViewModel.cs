using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.ViewModels.Account;

public class RegisterViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Username cannot be blank.")]
    [MinLength(2, ErrorMessage = "Username must be at least {1} characters.")]
    [MaxLength(20, ErrorMessage = "Username must be less than {1} characters.")]
    public string Username { get; set; } = null!;

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

    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters.")]
    [MaxLength(64, ErrorMessage = "Password must be less than {1} characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters.")]
    [MaxLength(64, ErrorMessage = "Password must be less than {1} characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string PasswordConfirmation { get; set; } = null!;

    public UserRole UserRole { get; set; }
}