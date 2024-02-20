using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.User;

public class UserUpdateDto
{
    [Required]
    [MinLength(4), MaxLength(20)]
    public string? Username { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email.")]
    [MaxLength(254)]
    public string? Email { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MinLength(2), MaxLength(20)]
    public string LastName { get; set; } = null!;

    [Required]
    public UserRole UserRole { get; set; }
}