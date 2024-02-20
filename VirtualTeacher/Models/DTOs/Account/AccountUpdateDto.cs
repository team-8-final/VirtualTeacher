using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Account;

public class AccountUpdateDto
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email")]
    [MaxLength(254)]
    public string? Email { get; set; }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string? FirstName { get; set; }

    [Required]
    [MinLength(2), MaxLength(20)]
    public string? LastName { get; set; }

    [Required]
    [MinLength(8), MaxLength(64)]
    public string? Password { get; set; }

    [MaxLength(32768)]
    public string? AvatarUrl { get; set; }
}