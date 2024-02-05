using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace VirtualTeacher.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class IsTeacherOrAdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = context.HttpContext.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Teacher" || c.Value == "Admin");

            if (!isAuthorized)
            {
                context.Result = new RedirectToRouteResult(new { controller = "Home", action = "Index" });
            }
        }
    }
}
