using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Models.DTOs;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers.API
{
    [Route("api/")]
    [Tags("Translator")]
    [ApiController]
    public class TranslatorApiController : ControllerBase
    {
        private readonly ITranslationService translationService;

        public TranslatorApiController(ITranslationService translationService)
        {
            this.translationService = translationService;
        }

        [HttpPost]
        [Route("translate/")]
        public async Task<IActionResult> Translate([FromBody] TranslationRequest request)
        {
            try
            {
                var translatedText = await translationService.TranslateText(request);

                return Ok(translatedText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
