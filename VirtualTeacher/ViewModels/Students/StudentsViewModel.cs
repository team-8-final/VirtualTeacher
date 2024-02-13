﻿using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;

namespace VirtualTeacher.ViewModels.Students
{
    public class StudentsViewModel
    {
        public List<Course>? FilteredCourses { get; set; }

        public List<UserResponseDto>? AllStudents { get; set; }
        public CourseQueryParameters? Parameters { get; set; }

    }
}
