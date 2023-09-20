using Apps.LanguageWeaver.Models.Dto;

namespace Apps.LanguageWeaver.Models.Responses.Dictionary;

public class ListDictionariesResponse
{
    public IEnumerable<DictionaryDto> Dictionaries { get; set; }
}