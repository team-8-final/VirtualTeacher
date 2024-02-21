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

        /// <summary>
        /// Translates a piece of text in chosen by the user language.
        /// </summary>
        /// <remarks>
        /// Supported languages and their input codes: Arabic (AR), Bulgarian (BG), Czech (CS), Danish (DA), German (DE), Greek (EL), English (EN), 
        /// Spanish (ES), Estonian (ET), Finnish (FI), French (FR), Hungarian (HU), Indonesian (ID), Italian (IT), Japanese (JA), Korean (KO), 
        /// Lithuanian (LT), Latvian (LV), Norwegian (Bokmål) (NB), Dutch (NL), Polish (PL), Portuguese (all Portuguese varieties mixed) (PT), Romanian (RO), 
        /// Russian (RU), Slovak (SK), Slovenian (SL), Swedish (SV), Turkish (TR), Ukrainian (UK), Chinese (ZH).
        /// </remarks> 
        /// <returns>
        /// The translated text
        /// </returns>
        /// <response code="200">The input text was successfully translated.</response>
        /// <response code="500">There was an error in the translation.</response>
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
