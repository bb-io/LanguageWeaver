using Blackbird.Applications.Sdk.Common;
using Apps.LanguageWeaver.Models.Requests.Base;

namespace Apps.LanguageWeaver.Models.Requests
{
    public class TranslateFileRequest : FileRequest
    {
        [Display("Source language")]
        public string SourceLanguage { get; set; }

        [Display("Target language")]
        public string TargetLanguage { get; set; }
    }
}
