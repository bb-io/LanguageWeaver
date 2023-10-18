using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Responses.Translation;

public class TranslateTextWithQeResponse
{
    [Display("Source language")] public string SourceLanguageId { get; set; }

    [Display("Target language")] public string TargetLanguageId { get; set; }
    public string Model { get; set; }
    public string Translation { get; set; }
    public QualityEstimationDto Quality { get; set; }
    public TranslationStats Stats { get; set; }

    public TranslateTextWithQeResponse(TranslationTextResultDto resultDto, TranslationStatusDto status)
    {
        SourceLanguageId = resultDto.SourceLanguageId;
        TargetLanguageId = resultDto.TargetLanguageId;
        Model = resultDto.Model;
        Translation = resultDto.Translation.First();
        Quality = status.QualityEstimation.First();
        Stats = status.TranslationStats;
    }
}