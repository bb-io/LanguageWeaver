using Blackbird.Applications.Sdk.Common;
using Apps.LanguageWeaver.Models.Requests.Base;

namespace Apps.LanguageWeaver.Models.Requests
{
    public class CreateInsightsForFileRequest : FileRequest
    {
        [Display("Source language")]
        public string SourceLanguage { get; set; }
    }
}
