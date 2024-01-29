using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Models.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [MinLength(4), MaxLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(254)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(64, ErrorMessage = "Password cannot be longer than 64 characters.")]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required]
        public UserRole UserRole { get; set; }
    }
}