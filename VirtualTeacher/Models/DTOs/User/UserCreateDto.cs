using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.enums;

namespace VirtualTeacher.Models.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(254)]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(8), MaxLength(64)]
        public string Password { get; set; } = null!;

        [Required]
        [MinLength(2), MaxLength(20)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MinLength(2), MaxLength(20)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(32768)]
        public string? AvatarUrl { get; set; }

        [Required]
        public UserRole UserRole { get; set; } = UserRole.Student;
    }
}
