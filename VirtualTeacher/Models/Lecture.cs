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

    [MaxLength(8192)] public string VideoLink { get; set; } = null!;

    [MaxLength(8192)] public string? AssignmentLink { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int TeacherId { get; set; }
    public User Teacher { get; set; } = null!;

    public IList<User> WatchedBy { get; set; } = new List<User>();
    public IList<Note> Notes { get; set; } = new List<Note>();
    public IList<Comment> Comments { get; set; } = new List<Comment>();
    public IList<Submission> Submissions { get; set; } = new List<Submission>();
}