using Apps.LanguageWeaver.Dto;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver
{
    public class LanguageWeaverClient : RestClient
    {
        public LanguageWeaverClient() :
            base(new RestClientOptions() { ThrowOnAnyError = true, BaseUrl = new Uri("https://api.languageweaver.com/v4/") })
        { }

        public TranslationStatusDto PollTransaltionOperation(string requestId, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var request = new LanguageWeaverRequest($"mt/translations/async/{requestId}",
                Method.Get, authenticationCredentialsProviders);
            var response = this.Get<TranslationStatusDto>(request);
            while (response?.TranslationStatus == "INIT" || response?.TranslationStatus == "TRANSLATING")
            {
                Task.Delay(2000);
                response = this.Get<TranslationStatusDto>(request);
            }
            if (response?.TranslationStatus != "DONE")
            {
                throw new Exception("Translation operation failed");
            }
            return response;
        }

        public IdentificationStatusDto PollIndentificationOperation(string requestId, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var request = new LanguageWeaverRequest($"multi-language-identification/async/{requestId}",
                Method.Get, authenticationCredentialsProviders);
            var response = this.Get<IdentificationStatusDto>(request);
            while (response?.Status == "INIT" || response?.Status == "IN_PROGRESS")
            {
                Task.Delay(2000);
                response = this.Get<IdentificationStatusDto>(request);
            }
            if (response?.Status != "DONE")
            {
                throw new Exception("Identification operation failed");
            }
            return response;
        }

        public InsightStatusDto PollInsightCreationOperation(string insightId, IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders)
        {
            var request = new LanguageWeaverRequest($"content-insights/{insightId}",
                Method.Get, authenticationCredentialsProviders);
            var response = this.Get<InsightStatusDto>(request);
            while (response?.ContentInsightsStatus == "INIT" ||
                   response?.ContentInsightsStatus == "ACCEPTED" ||
                   response?.ContentInsightsStatus == "IN_PROGRESS")
            {
                Task.Delay(2000);
                response = this.Get<InsightStatusDto>(request);
            }
            if (response?.ContentInsightsStatus != "DONE")
            {
                throw new Exception("Insight creation operation failed");
            }
            return response;
        }
    }
}
