using Apps.LanguageWeaver.DataSourceHandlers;
using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class TranslateTextWithQeRequest
{
    public string Text { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Dictionary IDs")]
    [DataSource(typeof(DictionaryDataHandler))]
    public IEnumerable<string>? Dictionaries { get; set; }

    [Display("Input format")]
    [DataSource(typeof(TextInputFormatDataHandler))]
    public string? InputFormat { get; set; }

    [Display("Translation mode")]
    [DataSource(typeof(TranslationModeDataHandler))]
    public string? TranslationMode { get; set; }

    [Display("Model")]
    public string? Model { get; set; }
}