using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels;
using VirtualTeacher.Models.Enums;

namespace VirtualTeacher.Controllers.MVC;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICourseService courseService;
    private readonly IUserService userService;

    public HomeController(ILogger<HomeController> logger, ICourseService courseService, IUserService userService)
    {
        _logger = logger;
        this.courseService = courseService;
        this.userService = userService;
    }

    // todo probably move to service
    public IActionResult Index()
    {
        HomeIndexViewModel viewModel = new HomeIndexViewModel();
        CourseQueryParameters parameters = new CourseQueryParameters();
        UserQueryParameters userParameters = new UserQueryParameters();

        viewModel.CoursesByDate = courseService.FilterCoursesBy(parameters).OrderByDescending(c => c.Id).Take(3).ToList();
        viewModel.CoursesByPopularity = courseService.FilterCoursesBy(parameters).OrderByDescending(c => c.EnrolledStudents.Count()).Take(3).ToList();
        viewModel.CoursesByRating = courseService.FilterCoursesBy(parameters).OrderByDescending(c => c.Ratings.Any() ? c.Ratings.Average(r => r.Value) : 0).Take(3).ToList();
        viewModel.Teachers = userService.FilterBy(userParameters).Where(u => u.UserRole == UserRole.Teacher).Take(3).ToList();

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}