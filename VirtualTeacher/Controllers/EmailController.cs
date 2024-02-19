using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Models.DTOs;


namespace VirtualTeacher.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            emailService.SendEmail(request);
            return Ok();
        }
    }
}
