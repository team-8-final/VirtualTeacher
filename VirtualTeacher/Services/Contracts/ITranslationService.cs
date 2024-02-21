using DeepL.Model;
using VirtualTeacher.Models.DTOs;

namespace VirtualTeacher.Services.Contracts
{
    public interface ITranslationService
    {
        Task<string> TranslateText(TranslationRequest request);
    }
}
