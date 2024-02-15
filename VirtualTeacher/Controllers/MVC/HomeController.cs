using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels;
using VirtualTeacher.Models.Enums;
using VirtualTeacher.Helpers;

namespace VirtualTeacher.Controllers.MVC;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICourseService courseService;
    private readonly IUserService userService;
    private readonly ModelMapper mapper;

    public HomeController(ILogger<HomeController> logger, ICourseService courseService, IUserService userService, ModelMapper mapper)
    {
        _logger = logger;
        this.courseService = courseService;
        this.userService = userService;
        this.mapper = mapper;
    }

    // todo probably move to service
    public IActionResult Index()
    {
        var topRatedCourses = courseService.GetTopRatedCourses();
        var newestCourses = courseService.GetNewestCourses();
        var popularCourses = courseService.GetPopularCourses();
        var teachers = userService.GetHomeTeachers();

        HomeIndexViewModel vm = mapper.MapHomeVM(newestCourses, topRatedCourses, popularCourses, teachers);

        return View(vm);
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