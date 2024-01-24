using VirtualTeacher.Models;
using VirtualTeacher.Models.enums;

namespace VirtualTeacher;
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
                new() { Id = 1, CourseId = 1, Title = "Lecture 1: The basics", Description = "Test description", VideoLink = "https://www.youtube.com/watch?v=Tqt7Zj-qAtk",},
                new() { Id = 2, CourseId = 1, Title = "Lecture 2: Next Level", Description = "description #2", VideoLink = "https://www.youtube.com/watch?v=X99fpJ2HB0A",},
            });


        modelBuilder.Entity<User>().HasData(
            new List<User>
            {
                new User { Id = 1, FirstName = "Admin", LastName = "Admin", Password = "securepass", Email = "admin@example.com", UserRole = UserRole.Admin, AvatarUrl="randomurl.com" },
                new User { Id = 2, FirstName = "John", LastName = "Doe", Password = "securepass", Email = "johndoe@example.com", UserRole = UserRole.Student, AvatarUrl="randomurl.com" },
                new User { Id = 3, FirstName = "Stevie", LastName = "Johnson", Password = "securepass", Email = "stevie@example.com", UserRole = UserRole.Teacher, AvatarUrl="randomurl.com" },

            });

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Teachers)
            .WithMany(teacher => teacher.Courses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseTeachers"));

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Students)
            .WithMany(student => student.CreatedCourses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseStudents"));

        modelBuilder.Entity<Assignment>()
            .HasMany(a => a.Teachers)
            .WithMany(t => t.CreatedAssignments)
            .UsingEntity<Dictionary<string, object>>(
                "AssignmentTeachers",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("TeachersId")
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Assignment>()
                    .WithMany()
                    .HasForeignKey("CreatedAssignmentsId")
                    .OnDelete(DeleteBehavior.Cascade)
            );

        modelBuilder.Entity<Assignment>()
            .HasOne(assignment => assignment.Student)
            .WithMany(user => user.Assignments);

        modelBuilder.Entity<Grade>()
            .HasOne(grade => grade.Assignment)
            .WithMany(assignment => assignment.Grades);

        modelBuilder.Entity<Grade>()
            .HasOne(grade => grade.Teacher)
            .WithMany(teacher => teacher.GivenGrades)
            .HasForeignKey(grade => grade.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lecture>()
            .HasOne(lecture => lecture.Course)
            .WithMany(course => course.Lectures);

        modelBuilder.Entity<Lecture>()
            .HasMany(lecture => lecture.Teacher)
            .WithMany(teacher => teacher.CreatedLectures)
            .UsingEntity(joinEntity => joinEntity.ToTable("LectureTeachers"));

        modelBuilder.Entity<Lecture>()
            .HasMany(lecture => lecture.Student)
            .WithMany(student => student.Lectures)
            .UsingEntity(joinEntity => joinEntity.ToTable("WatchedLectures"));

        modelBuilder.Entity<Lecture>()
            .HasMany(course => course.Assignments)
            .WithOne(assignment => assignment.Lecture);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Grade>()
            .HasIndex(g => new { g.AssignmentId, g.TeacherId })
            .IsUnique();
    }
}