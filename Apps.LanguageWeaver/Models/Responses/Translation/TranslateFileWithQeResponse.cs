using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageWeaver.Models.Responses.Translation
{
    public class TranslateFileWithQeResponse
    {
        public FileReference File { get; set; }
        public QualityEstimationDto Quality { get; set; }
        public TranslationStats Stats { get; set; }
    }
}
