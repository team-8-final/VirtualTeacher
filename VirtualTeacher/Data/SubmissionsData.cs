using VirtualTeacher.Models;

namespace VirtualTeacher.Data;

public static class SubmissionsData
{
    public static List<Submission> Seed()
    {
        return new List<Submission>
        {
            new() { Id = 1,  LectureId = 10, StudentId = 2, Grade = 50 },
            new() { Id = 2,  LectureId = 11, StudentId = 2, Grade = 60 },
            new() { Id = 3,  LectureId = 12, StudentId = 2, Grade = 10 },
            new() { Id = 4,  LectureId = 13, StudentId = 2, Grade = 90 },
            new() { Id = 5,  LectureId = 19, StudentId = 2, Grade = 100 },
            new() { Id = 6,  LectureId = 20, StudentId = 2, Grade = 90 },
            new() { Id = 7,  LectureId = 24, StudentId = 2, Grade = 60 },
            new() { Id = 8,  LectureId = 25, StudentId = 2, Grade = 80 },
            new() { Id = 9,  LectureId = 26, StudentId = 2, Grade = 90 },
            new() { Id = 10, LectureId = 27, StudentId = 2, Grade = 50 },
        };
    }
}