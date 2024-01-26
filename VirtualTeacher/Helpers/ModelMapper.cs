using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs.Comment;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.DTOs.User;

namespace VirtualTeacher.Helpers;

public class ModelMapper
{
    //User DTOs
    public User MapCreate(UserCreateDto dto)
    {
        return new User()
        {
            Email = dto.Email,
            Username = dto.Username,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = dto.Password,
            AvatarUrl = dto.AvatarUrl,
            UserRole = dto.UserRole
        };
    }

    public UserResponseDto MapResponse(User user)
    {
        return new UserResponseDto()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserRole = user.UserRole.ToString()
        };
    }

    public User MapUpdate(UserUpdateDto dto)
    {
        return new User()
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = dto.Password,
            AvatarUrl = dto.AvatarUrl
        };
    }

    // course DTOs
    public CourseResponseDto MapResponse(Course course)
    {
        return new CourseResponseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            StartingDate = course.StartingDate,
            CourseTopic = course.CourseTopic.ToString(),
            Published = course.Published,

            EnrolledStudents = new List<User>(course.EnrolledStudents),
            Lectures = new List<Lecture>(course.Lectures),
            Ratings = new List<Rating>(course.Ratings),
            ActiveTeachers = new List<User>(course.ActiveTeachers)
        };
    }

    public RatingResponseDto MapResponse(Rating rating)
    {
        return new RatingResponseDto
        {
            Rating = rating.Value,
            Course = rating.Course.Title,
            Student = rating.Student.Username
        };
    }

    //Comment DTOs

    public Comment MapCreate(CommentCreateDto dto, User author)
    {
        return new Comment()
        {
            CreatedOn = DateTime.Now,
            AuthorId = author.Id,
            Author = author,
            LectureId = dto.LectureId,
            Content = dto.Content
        };
    }

    public Comment MapUpdate(CommentUpdateDto dto)
    {
        return new Comment()
        {
            Content = dto.Content
        };
    }
}