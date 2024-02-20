using VirtualTeacher.Models;

namespace VirtualTeacher.Data;

public static class SubmissionsData
{
    public static List<Submission> Seed()
    {
        return new List<Submission>
        {
             //Submissions break if there are no files associated with the seed data

             new() { Id = 1, LectureId = 1, StudentId = 14, Grade = null },
             new() { Id = 2, LectureId = 2, StudentId = 14, Grade = null },
             new() { Id = 3, LectureId = 2, StudentId = 15, Grade = 10 },
             new() { Id = 4, LectureId = 1, StudentId = 15, Grade = 90 },

        };
    }
}