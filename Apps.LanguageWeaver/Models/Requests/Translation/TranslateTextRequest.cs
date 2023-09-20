using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class TranslateTextRequest
{
    public string Text { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }
}