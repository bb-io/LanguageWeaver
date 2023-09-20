using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Content;
using Apps.LanguageWeaver.Models.Responses.Content;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class ContentInsightActions : LanguageWeaverInvocable
{
    public ContentInsightActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Create content insights for file", Description = "Create content insights for a specific file")]
    public async Task<CreateInsightsDto> CreateInsightsForFile([ActionParameter] CreateInsightsForFileRequest input)
    {
        var endpoint = "content-insights";

        var request = new LanguageWeaverRequest(endpoint, Method.Post, Creds);
        request.AddParameter("sourceLanguage", input.SourceLanguage);
        request.AddFile("input", input.File.Bytes, input.FileName ?? input.File.Name);

        var createInsightResponse = await Client.ExecuteAsync<CreateInsightsDto>(request);

        Client.PollInsightCreationOperation(createInsightResponse.Data.ContentInsightsId, Creds);
        return createInsightResponse.Data;
    }

    [Action("Get content insights result", Description = "Get content insights result by ID")]
    public async Task<GetContentInsightsResponse> GetContentInsights(
        [ActionParameter] GetContentInsightsRequest input)
    {
        var endpoint = $"content-insights/{input.ContentInsightsId}/result";
        var resultRequest = new LanguageWeaverRequest(endpoint, Method.Get, Creds);
        var insightsDto = await Client.GetAsync<ContentInsightsDto>(resultRequest);

        return new(insightsDto);
    }
}