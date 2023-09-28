using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Apps.LanguageWeaver.Models.Requests.Base;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class TranslateFileRequest : FileRequest
{
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Input format")]
    [DataSource(typeof(InputFormatDataHandler))]
    public string? InputFormat { get; set; }
}