using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models
{
    public class LoginRequest
    {
        //[Required]
        //[EmailAddress(ErrorMessage = "Invalid email address.")]
        //public string Email { get; set; }

        [Required]
        [MinLength(4), MaxLength(20)]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password must be more than 8 characteds long")] //The password must be at least 8 symbols and
                                                                                  // (todo) should contain capital letter, digit, and special symbol
        [DataType(DataType.Password)]
        [MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
