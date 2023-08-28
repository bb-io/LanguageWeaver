using Apps.LanguageWeaver.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Responses
{
    public class TranslateTextResponse
    {
        public TranslateTextResponse(TranslationTextResultDto resultDto) {
            SourceLanguageId = resultDto.SourceLanguageId;
            TargetLanguageId = resultDto.TargetLanguageId;
            Model = resultDto.Model;
            Translation = resultDto.Translation.First();
        }

        [Display("Source language")]
        public string SourceLanguageId { get; set; }

        [Display("Target language")]
        public string TargetLanguageId { get; set; }
        public string Model { get; set; }
        public string Translation { get; set; }
    }
}
