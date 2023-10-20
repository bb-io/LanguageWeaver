using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Apps.LanguageWeaver.Models.Requests.Base;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class TranslateFileWithQeRequest : FileRequest
{
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }
    
    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [Display("Input format")]
    [DataSource(typeof(FileInputFormatDataHandler))]
    public string? InputFormat { get; set; }

    [Display("Translation mode")]
    [DataSource(typeof(TranslationModeDataHandler))]
    public string? TranslationMode { get; set; }

    [Display("Model")]
    public string? Model { get; set; }

    [Display("PDF Converter")]
    [DataSource(typeof(PdfConverterDataHandler))]
    public string? PdfConverter { get; set; }
}