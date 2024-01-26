using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Comment
{
    public class CommentUpdateDto
    {
        [Required]
        [MaxLength(512, ErrorMessage = "The content must be less than 512 characters long.")]
        public string Content { get; set; }
    }
}
