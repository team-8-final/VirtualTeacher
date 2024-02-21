using System.Collections.Generic;
using System.Reflection.Emit;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Models.DTOs.Course;
using VirtualTeacher.Models.DTOs.User;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.ViewModels;
using VirtualTeacher.ViewModels.Lectures;
using VirtualTeacher.ViewModels.Users;

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
            UserRole = user.UserRole.ToString(),
            EnrolledCourses = user.EnrolledCourses?.Select(c => c.Title).ToList() ?? new List<string>(),
        };
    }

    //todo probably useless???
    public User MapUpdate(UserUpdateDto dto)
    {
        return new User()
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Username = dto.Username,
            UserRole = dto.UserRole
        };
    }


    public List<UserResponseDto> MapStudentsToDto(List<User> objectUsers)
    {
        List<UserResponseDto> dtoStudents = new List<UserResponseDto>();
        foreach(var obj in objectUsers)
        {
            dtoStudents.Add(
                new UserResponseDto
                {
                     Id = obj.Id,
                    Username = obj.Username,
                    Email = obj.Email,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName
                });
        }
        return dtoStudents;
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
            VideoLink = course.VideoLink,
            Published = course.Published,

            EnrolledStudents = new List<string>(
                course.EnrolledStudents.Select(student => student.Username)),

            Lectures = new List<LectureTitleIdDto>(

                course.Lectures.Select(lecture => new LectureTitleIdDto
                {
                    Id = lecture.Id,
                    Title = lecture.Title
                }).ToList()),

            Ratings = new List<RatingResponseDto>(new List<RatingResponseDto>(
                course.Ratings.Select(MapResponse))),

            ActiveTeachers = new List<string>(
                course.ActiveTeachers.Select(teacher => teacher.Username))
        };
    }

    public CoursesListViewModel MapCourseList(PaginatedList<Course> courses, List<string> teachers, List<string> topics, CourseQueryParameters parameters)
    {
        return new CoursesListViewModel
        {
            Courses = courses,
            AllTeachers = teachers,
            AllTopics = topics,
            Parameters = parameters
        };
    }

    //Home VM
    public HomeIndexViewModel MapHomeVM(List<Course> newestCourses, List<Course> topRatedCourses, List<Course> popularCourses, List<User> teachers, 
        int usersCount, int coursesCount, int lecturesCount)
    {
        return new HomeIndexViewModel
        {
            CoursesByDate = newestCourses,
            CoursesByPopularity = popularCourses,
            CoursesByRating = topRatedCourses,
            Teachers = teachers,
            UsersCount = usersCount,
            CoursesCount = coursesCount,
            LecturesCount = lecturesCount
        };
    }

    //Course Update VM
    public CourseUpdateViewModel MapUpdateVM(Course course)
    {
        return new CourseUpdateViewModel
        {
            Title = course.Title,
            Description = course.Description,
            StartingDate = course.StartingDate,
            CourseTopic = course.CourseTopic,
            Published = course.Published,
            VideoLink = course.VideoLink
        };
    }

    //Lecture Update VM
    public LectureUpdateViewModel MapUpdateVM(Lecture lecture)
    {
        return new LectureUpdateViewModel
        {
            Title = lecture.Title,
            Description = lecture.Description,
            VideoLink = lecture.VideoLink
        };
    }

    //User Update VM
    public UserUpdateViewModel MapUpdateVM(User user)
    {
        return new UserUpdateViewModel
        {
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserRole = user.UserRole
        };
    }

    public RatingResponseDto MapResponse(Rating rating)
    {
        return new RatingResponseDto
        {
            Rating = rating.Value,
            Review = rating.Review,
            Student = rating.Student.Username
        };
    }

    public PaginatedList<Course> MapCoursesToPaginatedList(List<Course> courses, int pageSize, int pageNumber)
    {

        int totalPages = courses.Count() / 5;
        if (courses.Count() % pageSize != 0)
        {
            totalPages++; // Increment totalPages if there are remaining items to be displayed
        }

        courses = courses.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();

        return new PaginatedList<Course>(courses, totalPages, pageNumber);
    }


    //Lecture DTOs
    public LectureResponseDto MapResponse(Lecture lecture)
    {
        return new LectureResponseDto()
        {
            Id = lecture.Id,
            Title = lecture.Title,
            Description = lecture.Description,
            VideoLink = lecture.VideoLink,
            AssignmentLink = lecture.AssignmentLink,
            TeacherUsername = lecture.Teacher.Username,
            TeacherFirstName = lecture.Teacher.FirstName,
            TeacherLastName = lecture.Teacher.LastName
        };
    }

    public Lecture MapCreate(LectureCreateDto dto, int courseId)
    {
        return new Lecture()
        {
            Description = dto.Description,
            VideoLink = dto.VideoLink,
            CourseId = courseId
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

    //Teacher Application DTO
    public ApplicationResponseDto MapResponse(TeacherApplication application)
    {
        return new ApplicationResponseDto()
        {
            Id = application.Id,
            Student = application.Student.Username,
            StudentId = application.Student.Id,
            IsCompleted = application.IsCompleted
        };
    }
}