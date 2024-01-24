using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Comment
{
    public int Id { get; set; }

    [MaxLength(512, ErrorMessage = "The content must be less than 512 characters long.")]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}