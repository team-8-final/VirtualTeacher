using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Helpers.CustomAttributes;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.MVC
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [IsAdmin]
        [HttpGet]
        [Route("application/{applicationId}/{verdict}")]
        public IActionResult Resolve([FromRoute] int applicationId, [FromRoute] string verdict)
        {
            try
            {
                bool resolution = (verdict == "approve");
                applicationService.ResolveApplication(applicationId, resolution);

                return RedirectToAction("Index", "User");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status500InternalServerError;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }


        }

        //to do exceptions
        [HttpGet]
        [Route("account/application")]
        public IActionResult Create()
        {
            try
            {
                applicationService.CreateApplication();

                return RedirectToAction("Index", "Account");
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
