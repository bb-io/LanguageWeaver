using Apps.LanguageWeaver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.LanguageWeaver.Models.Requests.Dictionary;

public class DictionaryRequest
{
    [Display("Dictionary")]
    [DataSource(typeof(DictionaryDataHandler))]
    public string DictionaryId { get; set; }
}