using Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;
using Apps.LanguageWeaver.Models.Requests.Base;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Translation;

public class IdentifyFileLanguageRequest : FileRequest
{
    [DataSource(typeof(InputFormatDataHandler))]
    public string? Format { get; set; }
}