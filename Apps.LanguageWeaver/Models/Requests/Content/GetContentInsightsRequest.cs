using Blackbird.Applications.Sdk.Common;

namespace Apps.LanguageWeaver.Models.Requests.Content;

public class GetContentInsightsRequest
{
    [Display("Content insights ID")]
    public string ContentInsightsId { get; set; }
}