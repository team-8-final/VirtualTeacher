using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Models.DTOs.User;
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
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index(UserQueryParameters parameters)
        {
            ViewData["SortOrder"] = string.IsNullOrEmpty(parameters.SortOrder) ? "desc" : "";
            ViewData["UserCount"] = userService.GetUserCount();
            var users = userService.FilterBy(parameters);

            return View(users);
        }

        [IsAdmin]
        [HttpGet]
        [Route("{id}/ChangeRole/{roleId}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ChangeRole([FromRoute] int id, [FromRoute] int roleId)
        {
            try
            {
                userService.ChangeRole(id, roleId);

                return RedirectToAction("Index", "Users");
            }
            catch (InvalidUserInputException e)
            {
                TempData["StatusCode"] = StatusCodes.Status400BadRequest;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }

        [IsAdmin]
        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                userService.Delete(id);

                return RedirectToAction("Index", "Users");
            }
            catch (InvalidUserInputException e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
            catch (UnauthorizedOperationException e)
            {
                TempData["StatusCode"] = StatusCodes.Status401Unauthorized;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }
    }
}