using Microsoft.EntityFrameworkCore;
using VirtualTeacher.Models;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Data;
/**/
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Rating> Ratings { get; set; } = null!;

    public DbSet<Lecture> Lectures { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Submission> Submissions { get; set; } = null!;
    public DbSet<TeacherApplication> TeacherApplications { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(UsersData.Seed());

        modelBuilder.Entity<Course>().HasData(CoursesData.Seed());

        modelBuilder.Entity<Lecture>().HasData(new List<Lecture>(LecturesData.Seed()));

        modelBuilder.Entity<Comment>().HasData(CommentsData.Seed());

        modelBuilder.Entity<Note>().HasData(
            new List<Note>
            {
                new() { Id = 1, LectureId = 1, StudentId = 2, Content = "This is a note", },
                new() { Id = 2, LectureId = 2, StudentId = 1, Content = "This is also a note", },
            });

        modelBuilder.Entity<Rating>().HasData(
            new List<Rating>
            {
                new() { Id = 1, CourseId = 1, StudentId = 1, Value = 5, Review = "Very cool course!" },
                new() { Id = 2, CourseId = 2, StudentId = 1, Value = 1, Review = "Not good." },
                new() { Id = 3, CourseId = 1, StudentId = 8, Value = 1, Review = "Avoid this course." },
            });

        modelBuilder.Entity<TeacherApplication>().HasData(
            new List<TeacherApplication>
            {
                new() {Id = 1, StudentId = 15},
            });

        modelBuilder.Entity<Submission>().HasData(SubmissionsData.Seed());

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
                j => {
                    j.HasData(CourseTeacherData.Seed());
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
                    j.HasData(CourseStudentData.Seed());
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

        modelBuilder.Entity<TeacherApplication>()
             .HasOne(application => application.Student)
             .WithOne(student => student.TeacherApplication)
             .HasForeignKey<TeacherApplication>(application => application.StudentId)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(user => user.TeacherApplication)
            .WithOne(application => application.Student)
            .HasForeignKey<TeacherApplication>(application => application.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // uniques
        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Rating>()
            .HasIndex(rating => new { rating.StudentId, rating.CourseId })
            .IsUnique();
    }
}