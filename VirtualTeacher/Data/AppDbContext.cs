using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;
using VirtualTeacher.Models.enums;

namespace VirtualTeacher.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Lecture> Lectures { get; set; } = null!;

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
                new() { Id = 1, FirstName = "Admin", LastName = "Admin", Password = "securepass", Email = "admin@example.com", UserRole = UserRole.Admin, AvatarUrl="randomurl.com" },
                new() { Id = 2, FirstName = "John", LastName = "Doe", Password = "securepass", Email = "johndoe@example.com", UserRole = UserRole.Student, AvatarUrl="randomurl.com" },
                new() { Id = 3, FirstName = "Stevie", LastName = "Johnson", Password = "securepass", Email = "stevie@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },
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
            .HasOne(comment => comment.User)
            .WithMany(user => user.LectureComments)
            .HasForeignKey(comment => comment.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // uniques
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
    }
}