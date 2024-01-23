using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Lecture
{
    public int Id { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
    public string Title { get; set; } = null!;

    [MaxLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
    public string? Description { get; set; }

    [MaxLength(8192)]
    public string VideoLink { get; set; } = null!;

    public Course Course { get; set; } = null!;
    public IList<User> Student { get; set; } = null!;
    public IList<User> Teacher { get; set; } = null!;
    public IList<Note> Notes { get; set; } = null!;
    public IList<Assignment> Assignments { get; set; } = null!;
}