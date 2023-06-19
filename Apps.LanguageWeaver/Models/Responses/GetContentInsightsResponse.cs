using Apps.LanguageWeaver.Dto;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Models.Responses
{
    public class GetContentInsightsResponse
    {
        public GetContentInsightsResponse(ContentInsightsDto insightDto) 
        {
            ContentInsightsId = insightDto.ContentInsightsId;
            InputWordCount = insightDto.Stats.InputWordCount;
            InputCharCount = insightDto.Stats.InputCharCount;
            SourceLanguage = insightDto.SourceLanguage;
            Segments = insightDto.Summarization.Segments;
        }

        [Display("Content insights Id")]
        public string ContentInsightsId { get; set; }

        [Display("Input word count")]
        public int InputWordCount { get; set; }

        [Display("Input char count")]
        public int InputCharCount { get; set; }

        [Display("Source language")]
        public string SourceLanguage { get; set; }

        public List<Segment> Segments { get; set; }
    }
}
