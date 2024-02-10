using VirtualTeacher.Models;

namespace VirtualTeacher.Data;

public static class CommentsData
{
    public static List<Comment> Seed()
    {
        return new List<Comment>
        {
            // course 1
            new() { Id = 1, LectureId = 1, AuthorId = 6, Content = "Excellent breakdown of grammatical structures. Really appreciated the quizzes.", CreatedOn = DateTime.Parse("2024-02-12 02:08:17") },
            new() { Id = 2, LectureId = 2, AuthorId = 2, Content = "Helpful, but could use more context in examples.", CreatedOn = DateTime.Parse("2024-01-23 23:37:41") },
            new() { Id = 3, LectureId = 2, AuthorId = 13, Content = "This vocabulary list is a game-changer for beginners.", CreatedOn = DateTime.Parse("2024-02-10 09:57:14") },
            new() { Id = 4, LectureId = 2, AuthorId = 5, Content = "Nice selection of words. Maybe add some on technology?", CreatedOn = DateTime.Parse("2024-02-13 18:37:52") },
            new() { Id = 5, LectureId = 2, AuthorId = 8, Content = "This vocabulary list is a game-changer for beginners.", CreatedOn = DateTime.Parse("2024-01-30 10:45:40") },
            new() { Id = 6, LectureId = 2, AuthorId = 3, Content = "Nice selection of words. Maybe add some on technology?", CreatedOn = DateTime.Parse("2024-02-27 04:58:36") },
            new() { Id = 7, LectureId = 2, AuthorId = 4, Content = "Helpful, but could use more context in examples.", CreatedOn = DateTime.Parse("2024-01-17 03:01:53") },
            new() { Id = 8, LectureId = 2, AuthorId = 5, Content = "Helpful, but could use more context in examples.", CreatedOn = DateTime.Parse("2024-01-26 08:02:16") },
            new() { Id = 9, LectureId = 2, AuthorId = 5, Content = "The idioms section was super interesting!", CreatedOn = DateTime.Parse("2024-01-22 18:04:24") },
            new() { Id = 10, LectureId = 3, AuthorId = 1, Content = "Could we have more exercises on sentence variation?", CreatedOn = DateTime.Parse("2024-01-18 04:01:33") },
            new() { Id = 11, LectureId = 3, AuthorId = 4, Content = "Simple and effective teaching method. Thanks!", CreatedOn = DateTime.Parse("2024-02-22 18:03:02") },
            new() { Id = 12, LectureId = 3, AuthorId = 10, Content = "Just what I needed to start forming sentences on my own.", CreatedOn = DateTime.Parse("2024-02-24 03:05:59") },
            new() { Id = 13, LectureId = 3, AuthorId = 4, Content = "Could we have more exercises on sentence variation?", CreatedOn = DateTime.Parse("2024-02-21 11:18:30") },
            new() { Id = 14, LectureId = 3, AuthorId = 1, Content = "Just what I needed to start forming sentences on my own.", CreatedOn = DateTime.Parse("2024-02-06 17:44:51") },
            new() { Id = 15, LectureId = 3, AuthorId = 11, Content = "Simple and effective teaching method. Thanks!", CreatedOn = DateTime.Parse("2024-02-04 16:37:09") },
            new() { Id = 16, LectureId = 3, AuthorId = 11, Content = "Just what I needed to start forming sentences on my own.", CreatedOn = DateTime.Parse("2024-03-02 14:23:39") },
            new() { Id = 17, LectureId = 4, AuthorId = 2, Content = "Good start, but hoping for more in-depth conversations in future lectures.", CreatedOn = DateTime.Parse("2024-01-29 09:04:25") },
            new() { Id = 18, LectureId = 4, AuthorId = 9, Content = "A good foundation for everyday conversations.", CreatedOn = DateTime.Parse("2024-02-12 07:13:50") },
            new() { Id = 19, LectureId = 4, AuthorId = 8, Content = "Good start, but hoping for more in-depth conversations in future lectures.", CreatedOn = DateTime.Parse("2024-01-17 00:27:00") },

            // course 2
            new() { Id = 20, LectureId = 6, AuthorId = 2, Content = "These conversation examples are exactly what I was looking for.", CreatedOn = DateTime.Parse("2024-01-16 00:24:41") },
            new() { Id = 21, LectureId = 6, AuthorId = 6, Content = "These conversation examples are exactly what I was looking for.", CreatedOn = DateTime.Parse("2024-03-06 08:07:38") },
            new() { Id = 22, LectureId = 7, AuthorId = 11, Content = "Really brings the language to life with practical examples.", CreatedOn = DateTime.Parse("2024-03-09 17:35:14") },
            new() { Id = 23, LectureId = 7, AuthorId = 8, Content = "These conversation examples are exactly what I was looking for.", CreatedOn = DateTime.Parse("2024-02-09 05:50:07") },
            new() { Id = 24, LectureId = 7, AuthorId = 1, Content = "These conversation examples are exactly what I was looking for.", CreatedOn = DateTime.Parse("2024-01-25 14:04:55") },
            new() { Id = 25, LectureId = 9, AuthorId = 14, Content = "Would appreciate more varied scenarios to cover.", CreatedOn = DateTime.Parse("2024-01-22 01:34:14") },
            new() { Id = 26, LectureId = 9, AuthorId = 14, Content = "These conversation examples are exactly what I was looking for.", CreatedOn = DateTime.Parse("2024-02-09 17:35:45") },
            new() { Id = 27, LectureId = 9, AuthorId = 4, Content = "Really brings the language to life with practical examples.", CreatedOn = DateTime.Parse("2024-01-25 12:45:09") },
            new() { Id = 28, LectureId = 9, AuthorId = 13, Content = "Really brings the language to life with practical examples.", CreatedOn = DateTime.Parse("2024-01-27 04:37:11") },
            new() { Id = 29, LectureId = 9, AuthorId = 8, Content = "Really brings the language to life with practical examples.", CreatedOn = DateTime.Parse("2024-02-11 09:34:04") },

            // course 3
            new() { Id = 44, LectureId = 10, AuthorId = 4, Content = "Great advice on email etiquette; it's already making a difference.", CreatedOn = DateTime.Parse("2024-03-07 15:22:59") },
            new() { Id = 45, LectureId = 10, AuthorId = 13, Content = "The email templates were incredibly useful for structuring my own.", CreatedOn = DateTime.Parse("2024-03-04 03:15:42") },
            new() { Id = 46, LectureId = 10, AuthorId = 10, Content = "The email templates were incredibly useful for structuring my own.", CreatedOn = DateTime.Parse("2024-01-13 17:36:41") },
            new() { Id = 47, LectureId = 11, AuthorId = 2, Content = "Really appreciated the practical advice on using visuals.", CreatedOn = DateTime.Parse("2024-01-13 02:47:38") },
            new() { Id = 48, LectureId = 11, AuthorId = 9, Content = "Really appreciated the practical advice on using visuals.", CreatedOn = DateTime.Parse("2024-01-26 18:08:25") },
            new() { Id = 49, LectureId = 11, AuthorId = 11, Content = "Really appreciated the practical advice on using visuals.", CreatedOn = DateTime.Parse("2024-03-05 08:57:07") },
            new() { Id = 50, LectureId = 11, AuthorId = 1, Content = "Really appreciated the practical advice on using visuals.", CreatedOn = DateTime.Parse("2024-02-26 17:06:56") },
            new() { Id = 51, LectureId = 11, AuthorId = 8, Content = "Would benefit from more examples of successful presentations.", CreatedOn = DateTime.Parse("2024-02-11 12:30:32") },
            new() { Id = 52, LectureId = 11, AuthorId = 1, Content = "The tips on engaging the audience were game-changers for me.", CreatedOn = DateTime.Parse("2024-01-19 06:36:33") },
            new() { Id = 53, LectureId = 12, AuthorId = 10, Content = "Could use more case studies from real business negotiations.", CreatedOn = DateTime.Parse("2024-02-10 21:36:03") },
            new() { Id = 54, LectureId = 12, AuthorId = 3, Content = "Valuable insights into the psychology of negotiation.", CreatedOn = DateTime.Parse("2024-02-25 16:22:36") },
            new() { Id = 55, LectureId = 13, AuthorId = 13, Content = "Now I understand the importance of clear communication in a corporate setting.", CreatedOn = DateTime.Parse("2024-02-23 21:28:06") },
            new() { Id = 56, LectureId = 13, AuthorId = 1, Content = "The exercises on active listening transformed how I communicate with colleagues.", CreatedOn = DateTime.Parse("2024-02-20 18:15:56") },
            new() { Id = 57, LectureId = 13, AuthorId = 7, Content = "More examples of internal communication best practices would be helpful.", CreatedOn = DateTime.Parse("2024-02-10 04:58:38") },
            new() { Id = 58, LectureId = 13, AuthorId = 5, Content = "The exercises on active listening transformed how I communicate with colleagues.", CreatedOn = DateTime.Parse("2024-02-29 10:55:40") },
            new() { Id = 59, LectureId = 13, AuthorId = 12, Content = "Now I understand the importance of clear communication in a corporate setting.", CreatedOn = DateTime.Parse("2024-03-01 02:45:23") },
            new() { Id = 60, LectureId = 13, AuthorId = 4, Content = "More examples of internal communication best practices would be helpful.", CreatedOn = DateTime.Parse("2024-01-30 21:59:39") },
            new() { Id = 61, LectureId = 13, AuthorId = 4, Content = "More examples of internal communication best practices would be helpful.", CreatedOn = DateTime.Parse("2024-01-19 05:09:02") },
            new() { Id = 62, LectureId = 13, AuthorId = 11, Content = "The module on non-verbal communication was particularly useful.", CreatedOn = DateTime.Parse("2024-01-29 02:12:16") },
            new() { Id = 63, LectureId = 13, AuthorId = 10, Content = "The exercises on active listening transformed how I communicate with colleagues.", CreatedOn = DateTime.Parse("2024-03-04 04:56:24") },
        };
    }
}