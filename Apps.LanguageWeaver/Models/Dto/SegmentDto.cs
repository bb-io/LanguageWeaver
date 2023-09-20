using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Dto;

public class SegmentDto
{
    public string Text { get; set; }

    [Display("Line number")]
    public int LineNumber { get; set; }
    
    public double Score { get; set; }
}