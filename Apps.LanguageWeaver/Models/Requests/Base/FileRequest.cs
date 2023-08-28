using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.LanguageWeaver.Models.Requests.Base;

public class FileRequest
{
    public File File { get; set; }
    
    [Display("File name")]
    public string? FileName { get; set; }
}