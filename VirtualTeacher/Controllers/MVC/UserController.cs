using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.MVC
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [IsAdmin]
        [HttpGet]
        [Route("/Users")]
        public IActionResult Index(UserQueryParameters parameters)
        {
            ViewData["SortOrder"] = string.IsNullOrEmpty(parameters.SortOrder) ? "desc" : "";
            var users = userService.FilterBy(parameters);

            return View(users); 
        }
    }
}
