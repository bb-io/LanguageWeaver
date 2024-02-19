using Apps.LanguageWeaver.Models.Dto;

namespace Apps.LanguageWeaver.Models.Responses.Dictionary;

public record ListDictionaryTermsResponse(IEnumerable<DictionaryTermDto> Terms);