﻿using System.ComponentModel.DataAnnotations;

namespace VirtualTeacher.Models.DTOs.User
{
    public class UserUpdateDto
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string? FirstName { get; set; } 

        [Required]
        [MinLength(2), MaxLength(20)]
        public string? LastName { get; set; }

        [Required]
        [MinLength(8), MaxLength(64)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(32768)]
        public string? AvatarUrl { get; set; }
    }
}
