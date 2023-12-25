using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.LanguageWeaver.Models.Requests.Base;

public class FileRequest
{
    public FileReference File { get; set; }
    
    [Display("File name")]
    public string? FileName { get; set; }
}