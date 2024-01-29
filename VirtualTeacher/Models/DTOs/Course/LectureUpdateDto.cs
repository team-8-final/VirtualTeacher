﻿using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.Course
{
    public class LectureUpdateDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "The title must be at least 5 characters long.")]
        [MaxLength(50, ErrorMessage = "The title must be less than 50 characters long.")]
        public string Title { get; set; } = null!;

        [MaxLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
        public string? Description { get; set; }
        public string VideoLink { get; set; } = null!;
        public string AssignmentLink { get; set; } = null!;

    }
}
