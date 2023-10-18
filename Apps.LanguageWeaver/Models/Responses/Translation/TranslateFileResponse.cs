using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common;
using System.Reflection;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageWeaver.Models.Responses.Translation;

public class TranslateFileResponse
{
    public File File { get; set; }
    public TranslationStats Stats { get; set; }
}