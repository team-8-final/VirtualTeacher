using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Course
{
    public class CommentCreateDto
    {
        [Required]
        [MaxLength(512, ErrorMessage = "The content must be less than 512 characters long.")]
        public string Content { get; set; }
    }
}