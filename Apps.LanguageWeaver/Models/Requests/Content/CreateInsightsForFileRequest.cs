using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Apps.LanguageWeaver.Models.Requests.Base;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Content;

public class CreateInsightsForFileRequest : FileRequest
{
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }
}