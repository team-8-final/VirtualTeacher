using VirtualTeacher.Services.Contracts;
using DeepL;
using Org.BouncyCastle.Utilities.Encoders;
using DeepL.Model;
using VirtualTeacher.Models.DTOs;

namespace VirtualTeacher.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly string authKey;
        private readonly Translator translator;

        public TranslationService(IConfiguration configuration)
        {
            authKey = configuration["TranslatorConfig:AuthKey"];
            translator = new Translator(authKey);
        }

        public async Task<string> TranslateText(TranslationRequest request)
        {

            var translatedText = await translator.TranslateTextAsync(request.BaseText, null, request.TargetLanguage);

            return translatedText.Text;
        }

    }
}
