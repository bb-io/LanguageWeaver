namespace Apps.LanguageWeaver.Models.Dto;

public class TranslationStatusDto
{
    public string TranslationStatus { get; set; }
    public string InputFormat { get; set; }
    public string OutputFormat { get; set; }
    public TranslationStats TranslationStats { get; set; }
    public List<QualityEstimationDto>? QualityEstimation { get; set; }
    public List<ErrorDto>? Errors { get; set; }
  
}