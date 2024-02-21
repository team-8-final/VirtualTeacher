namespace VirtualTeacher.Models.DTOs
{
    public class TranslationRequest
    {
        public string BaseText { get; set; } = null!;
        public string TargetLanguage { get; set; } = null!;
    }
}
