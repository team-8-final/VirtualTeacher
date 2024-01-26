using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
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
                new() { Id = 1, Username = "admin", FirstName = "Admin", LastName = "Admin", Password = "securepass", Email = "admin@example.com", UserRole = UserRole.Admin, AvatarUrl="randomurl.com" },
                new() { Id = 2, Username = "johndoe", FirstName = "John", LastName = "Doe", Password = "securepass", Email = "johndoe@example.com", UserRole = UserRole.Student, AvatarUrl="randomurl.com" },
                new() { Id = 3, Username = "steviej", FirstName = "Stevie", LastName = "Johnson", Password = "securepass", Email = "stevie@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },
            });

        modelBuilder.Entity<Course>().HasData(
            new List<Course>
            {
                new() { Id = 1, Title = "English A1", Description = "Most basic English course.", StartingDate = DateTime.Now, CourseTopic = CourseTopic.English, Published = true},
                new() { Id = 2, Title = "Chinese B2", Description = "More advanced Chinese course.", StartingDate = DateTime.MinValue, CourseTopic = CourseTopic.Chinese, Published = true},
                new() { Id = 3, Title = "English C2", Description = "Most advanced English course.", StartingDate = DateTime.MaxValue, CourseTopic = CourseTopic.English, Published = true},
            });

        modelBuilder.Entity<Lecture>().HasData(
            new List<Lecture>
            {
                new() { Id = 1, TeacherId = 3, CourseId = 1, Title = "Lecture 1: The basics", Description = "Test description", VideoLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk", AssignmentLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk"},
                new() { Id = 2, TeacherId = 3, CourseId = 1, Title = "Lecture 2: Next Level", Description = "description #2", VideoLink = "https://www.youtube.com/watch?v=X99fpJ2HB0A", AssignmentLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk"},
            });

        modelBuilder.Entity<Comment>().HasData(
            new List<Comment>
            {
                new() { Id = 1, LectureId = 1, StudentId = 2, Content = "This is a comment", },
                new() { Id = 2, LectureId = 2, StudentId = 1, Content = "This is also a comment", },
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
                new() { Id = 1, LectureId = 1, StudentId = 2, SubmissionLink = "www.test.com", Grade = 100 },
                new() { Id = 2, LectureId = 2, StudentId = 1, SubmissionLink = "www.test.com", Grade = 100 },
            });

        modelBuilder.Entity<Course>()
            .HasMany(course => course.ActiveTeachers)
            .WithMany(teacher => teacher.CreatedCourses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseTeachers"));

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
            .HasOne(comment => comment.Student)
            .WithMany(user => user.LectureComments)
            .HasForeignKey(comment => comment.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // uniques
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}