using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models;

public class Comment
{
    public int Id { get; set; }

    [MaxLength(512, ErrorMessage = "The content must be less than 512 characters long.")]
    public string Content { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public int LectureId { get; set; }
    public Lecture Lecture { get; set; } = null!;

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;
    //public bool IsDeleted { get; set; } = false;
    public bool IsEdited { get; set; } = false;
    public DateTime EditDate { get; set; }
}