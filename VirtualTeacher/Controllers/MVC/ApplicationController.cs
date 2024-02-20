using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.MVC
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        //[IsAdmin]
        [HttpGet]
        [Route("application/{applicationId}/{resolution}")]
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

        //todo create
    }
}
