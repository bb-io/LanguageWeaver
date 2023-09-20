using Apps.LanguageWeaver.Constants;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Responses;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.Extensions.String;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.LanguageWeaver.Api;

public class LanguageWeaverClient : BlackBirdRestClient
{
    public LanguageWeaverClient(IEnumerable<AuthenticationCredentialsProvider> creds) :
        base(new()
        {
            BaseUrl = Urls.Api.ToUri()
        })
    {
        var client = new RestClient();
        var request = new RestRequest($"{Urls.Api}token", Method.Post);
        request.AddJsonBody(new
        {
            clientId = creds.Get(CredsNames.ClientId).Value,
            clientSecret = creds.Get(CredsNames.ClientSecret).Value,
        });
        var token = client.Execute<AccessTokenResponse>(request).Data.AccessToken;
        this.AddDefaultHeader("Authorization", $"Bearer {token}");
    }

    public void PollTransaltionOperation(string requestId,
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new LanguageWeaverRequest($"mt/translations/async/{requestId}",
            Method.Get, creds);
        var response = this.Get<TranslationStatusDto>(request);
        while (response?.TranslationStatus == "INIT" || response?.TranslationStatus == "TRANSLATING")
        {
            Task.Delay(2000);
            response = this.Get<TranslationStatusDto>(request);
        }

        if (response?.TranslationStatus != "DONE")
            throw new Exception("Translation operation failed");
    }

    public void PollIndentificationOperation(string requestId,
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new LanguageWeaverRequest($"multi-language-identification/async/{requestId}",
            Method.Get, creds);
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
    }

    public void PollInsightCreationOperation(string insightId,
        IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new LanguageWeaverRequest($"content-insights/{insightId}",
            Method.Get, creds);
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
    }
    
    protected override Exception ConfigureErrorException(RestResponse response)
    {
        if (string.IsNullOrWhiteSpace(response.Content))
            return new(response.StatusCode.ToString());
        
        var errors = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        return new(string.Join("; ", errors.Errors.Select(x => x.Description)));
    }
}