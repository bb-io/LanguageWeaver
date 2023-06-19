using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver
{
    public class LanguageWeaverRequest : RestRequest
    {
        public LanguageWeaverRequest(string endpoint, Method method, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders) : base(endpoint, method)
        {
            this.AddHeader("Authorization", $"Bearer {authenticationCredentialsProviders.First(p => p.KeyName == "apiKey").Value}");
        }
    }
}
