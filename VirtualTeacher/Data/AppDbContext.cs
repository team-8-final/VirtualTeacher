using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Data;
/**/
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;/**/
    public DbSet<Rating> Ratings { get; set; } = null!;

    public DbSet<Lecture> Lectures { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Submission> Submissions { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new() { Id = 1, Username = "admin", FirstName = "Admin", LastName = "Admin", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "admin@example.com", UserRole = UserRole.Admin, AvatarUrl="randomurl.com" },
                new() { Id = 2, Username = "johndoe", FirstName = "John", LastName = "Doe", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "johndoe@example.com", UserRole = UserRole.Student, AvatarUrl="randomurl.com" },
                new() { Id = 3, Username = "steviej", FirstName = "Stevie", LastName = "Johnson", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "stevie@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },
                new() { Id = 4, Username = "peter", FirstName = "Peter", LastName = "Sanders", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "peter@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },
                new() { Id = 5, Username = "michaelj", FirstName = "Michael", LastName = "Jordan", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "mj23@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },
                new() { Id = 6, Username = "stephc", FirstName = "Stephen", LastName = "Curry", Password = "qCs2FMBZ9j2q/7MYMah70BgC92dIHOMkTHsdoSP/G6ULPpc7yeyqwoBB5cm8f4QFy089RiV0q1ebCz8QsGa83w==", Email = "stephc@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },

            });

        modelBuilder.Entity<Course>().HasData(
            new List<Course>
            {
                new() { Id = 1, Title = "English A1", Description = "Most basic English course.", StartingDate = DateTime.Now, CourseTopic = CourseTopic.English, Published = true},
                new() { Id = 2, Title = "French B2", Description = "More advanced French course.", StartingDate = DateTime.MinValue, CourseTopic = CourseTopic.French, Published = true},
                new() { Id = 3, Title = "English C2", Description = "Most advanced English course.", StartingDate = DateTime.MaxValue, CourseTopic = CourseTopic.English, Published = true},
                new() { Id = 4, Title = "English A1", Description = "Most basic English course.", StartingDate = DateTime.Now, CourseTopic = CourseTopic.English, Published = true},
                new() { Id = 5, Title = "Chinese B2", Description = "More advanced Chinese course.", StartingDate = DateTime.MinValue, CourseTopic = CourseTopic.Chinese, Published = true},
                new() { Id = 6, Title = "French Beginner's Course", Description = "Learn the basics of the French language.", StartingDate = DateTime.MaxValue, CourseTopic = CourseTopic.French, Published = true},
                new() { Id = 7, Title = "Spanish Conversation Mastery", Description = "Improve your conversational skills in Spanish.", StartingDate = DateTime.Now.AddDays(30), CourseTopic = CourseTopic.Spanish, Published = true},
                new() { Id = 8, Title = "German Grammar Workshop", Description = "Deepen your understanding of German grammar.", StartingDate = DateTime.Now.AddDays(15), CourseTopic = CourseTopic.German, Published = true},
                new() { Id = 9, Title = "Italian for Travelers", Description = "Essential Italian phrases for travelers.", StartingDate = DateTime.Now.AddDays(10), CourseTopic = CourseTopic.Italian, Published = true},
                new() { Id = 10, Title = "Japanese Writing Practice", Description = "Practice writing in Japanese characters.", StartingDate = DateTime.Now.AddDays(25), CourseTopic = CourseTopic.Japanese, Published = true},
                new() { Id = 11, Title = "Russian Pronunciation Mastery", Description = "Master the pronunciation of Russian sounds.", StartingDate = DateTime.Now.AddDays(5), CourseTopic = CourseTopic.Russian, Published = true},
                new() { Id = 12, Title = "Portuguese Express Course", Description = "Quick course for learning Portuguese expressions.", StartingDate = DateTime.Now.AddDays(20), CourseTopic = CourseTopic.Portuguese, Published = true},
                new() { Id = 13, Title = "Arabic Script Fundamentals", Description = "Learn the basics of the Arabic script.", StartingDate = DateTime.Now.AddDays(15), CourseTopic = CourseTopic.Arabic, Published = true},
            });

        modelBuilder.Entity<Lecture>().HasData(
            new List<Lecture>
            {
                new() { Id = 1, TeacherId = 3, CourseId = 1, Title = "Lecture 1: The basics", Description = "Test description", VideoLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk", AssignmentLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk"},
                new() { Id = 2, TeacherId = 3, CourseId = 1, Title = "Lecture 2: Next Level", Description = "description #2", VideoLink = "https://www.youtube.com/watch?v=X99fpJ2HB0A", AssignmentLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk"},
                new() { Id = 3, TeacherId = 3, CourseId = 2, Title = "Local dialects of Mandarin 1", Description = "If you want to be able to buy potatos at the local viliga market in Chine in 1400, this the course for you", VideoLink = "https://www.youtube.com/watch?v=SUxGsjDEfvo", AssignmentLink = ""},
                new() { Id = 4, TeacherId = 3, CourseId = 2, Title = "Local dialects of Mandarin 2", Description = "If you want to be able to buy potatos at the local viliga market in Chine in 1400, this the course for you", VideoLink = "https://www.youtube.com/watch?v=SUxGsjDEfvo", AssignmentLink = ""},
                new() { Id = 5, TeacherId = 3, CourseId = 2, Title = "Local dialects of Mandarin 3", Description = "If you want to be able to buy potatos at the local viliga market in Chine in 1400, this the course for you", VideoLink = "https://www.youtube.com/watch?v=SUxGsjDEfvo", AssignmentLink = ""},
                new() { Id = 6, TeacherId = 3, CourseId = 2, Title = "Local dialects of Mandarin 4", Description = "If you want to be able to buy potatos at the local viliga market in Chine in 1400, this the course for you", VideoLink = "https://www.youtube.com/watch?v=SUxGsjDEfvo", AssignmentLink = ""},
            });

        modelBuilder.Entity<Comment>().HasData(
            new List<Comment>
            {
                new() { Id = 1, LectureId = 1, AuthorId = 2, Content = "This is a comment", },
                new() { Id = 2, LectureId = 2, AuthorId = 1, Content = "This is also a comment", },
            });

        modelBuilder.Entity<Note>().HasData(
            new List<Note>
            {
                new() { Id = 1, LectureId = 1, StudentId = 2, Content = "This is a note", },
                new() { Id = 2, LectureId = 2, StudentId = 1, Content = "This is also a note", },
            });

        modelBuilder.Entity<Rating>().HasData(
            new List<Rating>
            {
                new() { Id = 1, CourseId = 1, StudentId = 1, Value = 5, },
                new() { Id = 2, CourseId = 2, StudentId = 1, Value = 1, },
                new() { Id = 3, CourseId = 1, StudentId = 2, Value = 1, },
            });

        modelBuilder.Entity<Submission>().HasData(
            new List<Submission>
            {
                new() { Id = 1, LectureId = 1, StudentId = 2, Grade = 100 },
                new() { Id = 2, LectureId = 2, StudentId = 1, Grade = 100 },
            });

        modelBuilder.Entity<Course>()
            .HasMany(course => course.ActiveTeachers)
            .WithMany(teacher => teacher.CreatedCourses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseTeachers"));

        // adds seed data to the CourseTeachers join table
        modelBuilder.Entity<Course>()
            .HasMany(course => course.ActiveTeachers)
            .WithMany(teacher => teacher.CreatedCourses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseTeachers",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("ActiveTeachersId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Course>()
                    .WithMany()
                    .HasForeignKey("CoursesId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasData(
                        new { CoursesId = 1, ActiveTeachersId = 3 },
                        new { CoursesId = 2, ActiveTeachersId = 3 },
                        new { CoursesId = 2, ActiveTeachersId = 2 },
                        new { CoursesId = 3, ActiveTeachersId = 1 },
                        new { CoursesId = 4, ActiveTeachersId = 1 },
                        new { CoursesId = 5, ActiveTeachersId = 1 },
                        new { CoursesId = 6, ActiveTeachersId = 1 },
                        new { CoursesId = 7, ActiveTeachersId = 1 },
                        new { CoursesId = 8, ActiveTeachersId = 3 },
                        new { CoursesId = 9, ActiveTeachersId = 3 },
                        new { CoursesId = 10, ActiveTeachersId = 3 },
                        new { CoursesId = 11, ActiveTeachersId = 3 },
                        new { CoursesId = 12, ActiveTeachersId = 1 },
                        new { CoursesId = 13, ActiveTeachersId = 1 }
                    );
                }
            );

        modelBuilder.Entity<Course>()
            .HasMany(course => course.EnrolledStudents)
            .WithMany(student => student.EnrolledCourses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseStudents"));

        modelBuilder.Entity<Lecture>()
            .HasOne(lecture => lecture.Teacher)
            .WithMany(teacher => teacher.CreatedLectures);

        modelBuilder.Entity<Lecture>()
            .HasMany(lecture => lecture.WatchedBy)
            .WithMany(student => student.WatchedLectures)
            .UsingEntity<Dictionary<string, object>>(
                "WatchedLectures",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("WatchedById")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Lecture>()
                    .WithMany()
                    .HasForeignKey("LectureId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        // adds enrolled students seed data
        modelBuilder.Entity<Course>()
            .HasMany(course => course.EnrolledStudents)
            .WithMany(student => student.EnrolledCourses)
            .UsingEntity<Dictionary<string, object>>(
                "CourseStudents",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Course>()
                    .WithMany()
                    .HasForeignKey("CourseId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasData(
                        new { StudentId = 1, CourseId = 1 },
                        new { StudentId = 1, CourseId = 2 },
                        new { StudentId = 1, CourseId = 3 },
                        new { StudentId = 2, CourseId = 1 },
                        new { StudentId = 2, CourseId = 3 },
                        new { StudentId = 3, CourseId = 2 }
                    );
                }
            );

        modelBuilder.Entity<Submission>()
            .HasOne(submission => submission.Student)
            .WithMany(student => student.Submissions)
            .HasForeignKey(student => student.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Note>()
            .HasOne(note => note.Student)
            .WithMany(student => student.Notes)
            .HasForeignKey(note => note.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(comment => comment.Author)
            .WithMany(user => user.LectureComments)
            .HasForeignKey(comment => comment.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        // uniques
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Rating>()
            .HasIndex(rating => new { rating.StudentId, rating.CourseId })
            .IsUnique();
    }
}