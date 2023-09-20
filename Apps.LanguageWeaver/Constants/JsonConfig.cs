using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Apps.LanguageWeaver.Constants;

public static class JsonConfig
{
    public static JsonSerializerSettings Settings => new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        ContractResolver = new DefaultContractResolver()
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    };
}