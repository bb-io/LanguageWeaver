namespace Apps.LanguageWeaver.Models.Dto;

public class ContentInsightsDto
{
    public string ContentInsightsId { get; set; }
    public Stats Stats { get; set; }
    public string SourceLanguage { get; set; }
    public Summarization Summarization { get; set; }
}

public class Stats
{
    public int InputWordCount { get; set; }
    public int InputCharCount { get; set; }
}

public class Summarization
{
    public IEnumerable<SegmentDto> Segments { get; set; }
}