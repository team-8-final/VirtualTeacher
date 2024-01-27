using VirtualTeacher.Models;
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

            EnrolledStudents = new List<string>(
                course.EnrolledStudents.Select(student => student.Username)),

            Lectures = new List<string>(
                course.Lectures.Select(lecture => lecture.Title)),

            Ratings = new List<RatingResponseDto>(new List<RatingResponseDto>(
                course.Ratings.Select(MapResponse))),

            ActiveTeachers = new List<string>(
                course.ActiveTeachers.Select(teacher => teacher.Username))
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
            //LectureId = dto.LectureId,
            Content = dto.Content
        };
    }

    public CommentResponseDto MapResponse(Comment comment)
    {
        return new CommentResponseDto()
        {
            Id = comment.Id,
            AuthorUsername = comment.Author.Username,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn
        };
    }
}