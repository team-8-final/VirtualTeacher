using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.ViewModels;

namespace VirtualTeacher.Controllers.MVC
{
    public class TranslatorController : Controller
    {
        private readonly ITranslationService translationService;

        public TranslatorController(ITranslationService translationService)
        {
            this.translationService = translationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Translate([FromForm] string originalText, [FromForm] string language)
        {
            try
            {
                TranslationRequest request = new TranslationRequest()
                {
                    BaseText = originalText,
                    TargetLanguage = language
                };

                var result = await translationService.TranslateText(request);

                TempData["TranslationResult"] = result;

                return RedirectToAction("Index", "Translator");
            }
            catch (Exception e)
            {
                TempData["StatusCode"] = StatusCodes.Status400BadRequest;
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction("Error", "Shared");
            }
        }
    }
}
