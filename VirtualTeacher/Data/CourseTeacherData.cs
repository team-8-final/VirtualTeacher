namespace VirtualTeacher.Data;

public static class CourseTeacherData
{
    public static List<object> Seed()
    {
        return new List<object>
        {
            new { CoursesId = 1, ActiveTeachersId = 2 },
            new { CoursesId = 2, ActiveTeachersId = 3 },
            new { CoursesId = 3, ActiveTeachersId = 4 },
            new { CoursesId = 4, ActiveTeachersId = 5 },
            new { CoursesId = 5, ActiveTeachersId = 6 },
            new { CoursesId = 6, ActiveTeachersId = 7 },
            new { CoursesId = 7, ActiveTeachersId = 8 },
            new { CoursesId = 8, ActiveTeachersId = 9 },
            new { CoursesId = 9, ActiveTeachersId = 10 },
            new { CoursesId = 10, ActiveTeachersId = 2 },
            new { CoursesId = 11, ActiveTeachersId = 3 },
            new { CoursesId = 12, ActiveTeachersId = 4 },
            new { CoursesId = 13, ActiveTeachersId = 5 },
            new { CoursesId = 14, ActiveTeachersId = 6 },
            new { CoursesId = 15, ActiveTeachersId = 7 },
            new { CoursesId = 16, ActiveTeachersId = 8 },
            new { CoursesId = 17, ActiveTeachersId = 9 },
            new { CoursesId = 18, ActiveTeachersId = 10 },
            new { CoursesId = 19, ActiveTeachersId = 2 },
            new { CoursesId = 20, ActiveTeachersId = 3 },
            new { CoursesId = 21, ActiveTeachersId = 4 },
            new { CoursesId = 22, ActiveTeachersId = 5 },
            new { CoursesId = 23, ActiveTeachersId = 6 },
            new { CoursesId = 24, ActiveTeachersId = 7 },
            new { CoursesId = 25, ActiveTeachersId = 8 },
            new { CoursesId = 26, ActiveTeachersId = 9 },
            new { CoursesId = 27, ActiveTeachersId = 10 },
            new { CoursesId = 28, ActiveTeachersId = 2 },
            new { CoursesId = 29, ActiveTeachersId = 3 },
            new { CoursesId = 30, ActiveTeachersId = 4 },
            // Courses with 2 teachers
            new { CoursesId = 11, ActiveTeachersId = 6 },
            new { CoursesId = 2, ActiveTeachersId = 5 },
            new { CoursesId = 23, ActiveTeachersId = 2 },
            new { CoursesId = 18, ActiveTeachersId = 6 },
            new { CoursesId = 29, ActiveTeachersId = 10 }
        };
    }
}