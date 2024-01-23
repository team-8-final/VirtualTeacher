using System.ComponentModel.DataAnnotations;
using VirtualTeacher.Models.enums;

namespace VirtualTeacher.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
    public string Title { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public DateTime? StartingDate { get; set; }

    public Topic Topic { get; set; }

    public IList<User> Teachers { get; set; } = null!;
    public IList<User> Students { get; set; } = null!;
    public IList<Lecture> Lectures { get; set; } = null!;
    public IList<Rating> Ratings { get; set; } = null!;
}