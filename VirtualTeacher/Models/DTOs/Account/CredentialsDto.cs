using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Account;

public class CredentialsDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MinLength(4), MaxLength(254)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password field is required.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(64, ErrorMessage = "Password cannot be longer than 64 characters.")]
    public string Password { get; set; } = null!;
}