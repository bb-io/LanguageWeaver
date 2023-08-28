using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Requests
{
    public class TranslateTextRequest
    {
        public string Text { get; set; }

        [Display("Source language")]
        public string SourceLanguage { get; set; }

        [Display("Target language")]
        public string TargetLanguage { get; set; }
    }
}
