using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class IdentifyTextLanguageRequest
{
    public string Text { get; set; }
    
    [DataSource(typeof(TextInputFormatDataHandler))]
    public string? Format { get; set; }
}