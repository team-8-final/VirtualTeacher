using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.ViewModels.Account;

public class LoginViewModel
{
    [EmailAddress]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email cannot be blank.")]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password cannot be blank.")]
    public string Password { get; set; } = null!;

    public bool RememberLogin { get; set; }
}