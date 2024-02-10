using VirtualTeacher.Models;

namespace VirtualTeacher.Data;

public static class SubmissionsData
{
    public static List<Submission> Seed()
    {
        return new List<Submission>
        {
            // Submissions break if there are no files associated with the seed data

            // new() { Id = 1,  LectureId = 1, StudentId = 1, Grade = 50 },
            // new() { Id = 2,  LectureId = 1, StudentId = 2, Grade = 60 },
            // new() { Id = 3,  LectureId = 1, StudentId = 3, Grade = 10 },
            // new() { Id = 4,  LectureId = 2, StudentId = 1, Grade = 90 },
            // new() { Id = 5,  LectureId = 3, StudentId = 1, Grade = 100 },
            // new() { Id = 6,  LectureId = 4, StudentId = 2, Grade = 90 },
            // new() { Id = 7,  LectureId = 5, StudentId = 2, Grade = 60 },
            // new() { Id = 8,  LectureId = 6, StudentId = 2, Grade = 80 },
            // new() { Id = 9,  LectureId = 7, StudentId = 2, Grade = 90 },
            // new() { Id = 10, LectureId = 8, StudentId = 2, Grade = 50 },
            // new() { Id = 11,  LectureId = 10, StudentId = 2, Grade = 50 },
            // new() { Id = 12,  LectureId = 11, StudentId = 2, Grade = 60 },
            // new() { Id = 13,  LectureId = 12, StudentId = 2, Grade = 10 },
            // new() { Id = 14,  LectureId = 13, StudentId = 2, Grade = 90 },
            // new() { Id = 15,  LectureId = 19, StudentId = 2, Grade = 100 },
            // new() { Id = 16,  LectureId = 20, StudentId = 2, Grade = 90 },
            // new() { Id = 17,  LectureId = 24, StudentId = 2, Grade = 60 },
            // new() { Id = 18,  LectureId = 25, StudentId = 2, Grade = 80 },
            // new() { Id = 19,  LectureId = 26, StudentId = 2, Grade = 90 },
            // new() { Id = 20, LectureId = 27, StudentId = 2, Grade = 50 },
        };
    }
}