using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageWeaver
{
    public class LanguageWeaverRequest : RestRequest
    {
        public LanguageWeaverRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
        }
    }
}
