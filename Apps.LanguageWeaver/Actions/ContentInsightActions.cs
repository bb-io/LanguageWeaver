using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Content;
using Apps.LanguageWeaver.Models.Responses.Content;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using Apps.LanguageWeaver.Actions.Base;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class ContentInsightActions : BaseActions
{
    public ContentInsightActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    [Action("Get file content insights", Description = "Create content insights for a specific file")]
    public async Task<GetContentInsightsResponse> CreateInsightsForFile([ActionParameter] CreateInsightsForFileRequest input)
    {
        var request = new LanguageWeaverRequest("content-insights", Method.Post);
        var fileBytes = await ConvertToByteArray(input.File);
        request.AddParameter("sourceLanguage", input.SourceLanguage);
        request.AddFile("input", fileBytes, input.FileName ?? input.File.Name);

        var createInsightResponse = await Client.ExecuteAsync<CreateInsightsDto>(request);

        Client.PollInsightCreationOperation(createInsightResponse.Data.ContentInsightsId, Creds);

        var resultRequest = new LanguageWeaverRequest($"content-insights/{createInsightResponse.Data.ContentInsightsId}/result", Method.Get);
        var insightsDto = await Client.GetAsync<ContentInsightsDto>(resultRequest);

        return new(insightsDto);
    }
}