using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageWeaver.Models.Responses.Translation;

public class TranslateFileResponse
{
    public FileReference File { get; set; }
    public TranslationStats Stats { get; set; }
}