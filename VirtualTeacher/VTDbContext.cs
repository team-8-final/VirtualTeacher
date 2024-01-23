using VirtualTeacher.Models;

namespace VirtualTeacher;
using Microsoft.EntityFrameworkCore;

public class VTDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;

    public VTDbContext(DbContextOptions<VTDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Teachers)
            .WithMany(user => user.Courses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseTeachers"));

        modelBuilder.Entity<Course>()
            .HasMany(course => course.Students)
            .WithMany(user => user.CreatedCourses)
            .UsingEntity(joinEntity => joinEntity.ToTable("CourseStudents"));

        modelBuilder.Entity<Lecture>()
            .HasMany(course => course.Assignments)
            .WithOne(assignment => assignment.Lecture);

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
            .WithMany(user => user.CreatedLectures)
            .UsingEntity(joinEntity => joinEntity.ToTable("LectureTeachers"));

        modelBuilder.Entity<Lecture>()
            .HasMany(lecture => lecture.Student)
            .WithMany(user => user.Lectures)
            .UsingEntity(joinEntity => joinEntity.ToTable("LectureStudents"));

        modelBuilder.Entity<Rating>()
            .HasOne(rating => rating.Course)
            .WithMany(course => course.Ratings);

        modelBuilder.Entity<Rating>()
            .HasOne(rating => rating.User)
            .WithMany(user => user.Ratings);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        modelBuilder.Entity<Grade>()
            .HasIndex(g => new { g.AssignmentId, g.TeacherId })
            .IsUnique();
    }
}