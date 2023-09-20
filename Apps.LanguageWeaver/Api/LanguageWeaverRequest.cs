using RestSharp;

namespace Apps.LanguageWeaver.Api;

public class LanguageWeaverRequest : RestRequest
{
    public LanguageWeaverRequest(string endpoint, Method method) : base(endpoint, method)
    {
    }
}