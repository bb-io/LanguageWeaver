using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Dto
{
    public class ContentInsightsDto
    {
        public string ContentInsightsId { get; set; }
        public Stats Stats { get; set; }
        public string SourceLanguage { get; set; }
        public Summarization Summarization { get; set; }
    }

    public class Segment
    {
        public string Text { get; set; }

        [Display("Line number")]
        public int LineNumber { get; set; }
        public double Score { get; set; }
    }

    public class Stats
    {
        public int InputWordCount { get; set; }
        public int InputCharCount { get; set; }
    }

    public class Summarization
    {
        public List<Segment> Segments { get; set; }
    }
}
