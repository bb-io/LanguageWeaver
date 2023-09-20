using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Dictionary;

public class CreateDictionaryRequest
{
    public string Name { get; set; }
    
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string Source { get; set; }

    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string Target { get; set; }
    
    public string? Description { get; set; }
}