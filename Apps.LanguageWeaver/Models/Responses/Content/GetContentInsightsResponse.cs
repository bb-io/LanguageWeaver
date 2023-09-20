using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Responses.Content;

public class GetContentInsightsResponse
{
    [Display("Content insights ID")]
    public string ContentInsightsId { get; set; }

    [Display("Input word count")]
    public int InputWordCount { get; set; }

    [Display("Input char count")]
    public int InputCharCount { get; set; }

    [Display("Source language")]
    public string SourceLanguage { get; set; }

    public IEnumerable<SegmentDto> Segments { get; set; }
    
    public GetContentInsightsResponse(ContentInsightsDto insightDto) 
    {
        ContentInsightsId = insightDto.ContentInsightsId;
        InputWordCount = insightDto.Stats.InputWordCount;
        InputCharCount = insightDto.Stats.InputCharCount;
        SourceLanguage = insightDto.SourceLanguage;
        Segments = insightDto.Summarization.Segments;
    }
}