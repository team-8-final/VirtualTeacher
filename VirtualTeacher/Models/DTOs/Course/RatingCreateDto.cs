using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Course;

public class RatingCreateDto
{
    public double Value { get; set; }
    [MinLength(5, ErrorMessage = "The rating review must be at least 5 characters long.")]
    [MaxLength(70, ErrorMessage = "The rating review must be less than 70 characters long.")]
    public string? Review { get; set; }
}