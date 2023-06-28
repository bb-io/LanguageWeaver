using Apps.LanguageWeaver.Dto;
using Apps.LanguageWeaver.Models.Requests;
using Apps.LanguageWeaver.Models.Responses;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Actions
{
    [ActionList]
    public class ContentInsightActions
    {
        [Action("Create content insights for file", Description = "Create content insights for file")]
        public CreateInsightsDto CreateInsightsForFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] CreateInsightsForFileRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
            var request = new LanguageWeaverRequest("content-insights", Method.Post, authenticationCredentialsProviders);
            request.AddParameter("sourceLanguage", input.SourceLanguage);
            request.AddFile("input", input.File, input.Filename);
            var createInsightResponse = client.Execute<CreateInsightsDto>(request).Data;
            client.PollInsightCreationOperation(createInsightResponse.ContentInsightsId, authenticationCredentialsProviders);
            return createInsightResponse;
        }

        [Action("Get content insights result", Description = "Get content insights result by id")]
        public GetContentInsightsResponse GetContentInsights(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] GetContentInsightsRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
            var resultRequest = new LanguageWeaverRequest($"content-insights/{input.ContentInsightsId}/result", Method.Get, authenticationCredentialsProviders);
            var insightsDto = client.Get<ContentInsightsDto>(resultRequest);
            return new GetContentInsightsResponse(insightsDto);
        }
    }
}
