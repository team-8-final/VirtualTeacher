using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.ViewModels.Account;

public class PasswordViewModel
{
    public string? Username { get; set; }

    [DataType(DataType.Password)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    public string CurrentPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters.")]
    [MaxLength(64, ErrorMessage = "Password must be less than {1} characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string NewPassword { get; set; } = null!;

    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least {1} characters.")]
    [MaxLength(64, ErrorMessage = "Password must be less than {1} characters.")]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
    public string ConfirmNewPassword { get; set; } = null!;

    public string AvatarUrl { get; set; } = "";
}