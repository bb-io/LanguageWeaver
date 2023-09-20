using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.LanguageWeaver.DataSourceHandlers.EnumHandlers;

public class InputFormatDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"HTML", "HTML"},
        {"PLAIN", "Plain text (UTF8)"},
        {"XLINE", "XLINE"},
        {"TMX", "TMX"},
        {"XLIFF", "XLIFF"},
        {"BCM", "BCM"},
        {"XML", "XML"},
        {"SDLXML", "SDLXML"}
    };
}