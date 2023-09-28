using System.Net.Mime;
using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Translation;
using Apps.LanguageWeaver.Models.Responses.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class TranslationActions : LanguageWeaverInvocable
{
    public TranslationActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    [Action("Translate text", Description = "Translate specific text")]
    public async Task<TranslateTextResponse> TranslateText([ActionParameter] TranslateTextRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);
        request.AddJsonBody(new
        {
            sourceLanguageId = input.SourceLanguage,
            targetLanguageId = input.TargetLanguage,
            input = new[] { input.Text },
            model = "generic"
        });

        var response = await Translate(request);
        var translation = JsonConvert.DeserializeObject<TranslationTextResultDto>(response.Content);

        return new(translation);
    }

    [Action("Translate file", Description = "Translate file")]
    public async Task<TranslateFileResponse> TranslateFile([ActionParameter] TranslateFileRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);

        var fileName = input.FileName ?? input.File.Name;
        request.AddParameter("sourceLanguageId", input.SourceLanguage);
        request.AddParameter("targetLanguageId", input.TargetLanguage);
        request.AddParameter("model", "generic");
        request.AddFile("input", input.File.Bytes, fileName);

        if (input.InputFormat != null)
        {
            request.AddParameter("inputFormat", input.InputFormat);
        }

        var translatedFile = await Translate(request);

        return new()
        {
            File = new(translatedFile.RawBytes ?? Array.Empty<byte>())
            {
                Name = fileName,
                ContentType = MediaTypeNames.Application.Octet
            }
        };
    }

    [Action("Identify text language", Description = "Identify language of the given text")]
    public Task<IdentifyTextLanguageResponse> IdentifyTextLanguage(
        [ActionParameter] IdentifyTextLanguageRequest input)
    {
        var endpoint = "multi-language-identification/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);
        request.AddJsonBody(new
        {
            input = input.Text,
            inputFormat = input.Format ?? "PLAIN"
        });

        return IdentifyLanguage(request);
    }

    [Action("Identify file language", Description = "Identify file language")]
    public Task<IdentifyTextLanguageResponse> IdentifyFileLanguage(
        [ActionParameter] IdentifyFileLanguageRequest input)
    {
        var endpoint = "multi-language-identification/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post)
            .AddFile("input", input.File.Bytes, input.FileName ?? input.File.Name)
            .AddParameter("inputFormat", input.Format ?? "PLAIN");

        return IdentifyLanguage(request);
    }

    #region Utils

    private async Task<IdentifyTextLanguageResponse> IdentifyLanguage(RestRequest request)
    {
        var identificationCreateResponse = await Client.ExecuteAsync<CreateTranslationDto>(request);

        var requestId = identificationCreateResponse.Data.RequestId;
        Client.PollIndentificationOperation(requestId, Creds);

        var resultEndpoint = $"multi-language-identification/async/{requestId}/result";
        var resultRequest = new LanguageWeaverRequest(resultEndpoint, Method.Get);

        return await Client.GetAsync<IdentifyTextLanguageResponse>(resultRequest);
    }

    private async Task<RestResponse> Translate(RestRequest request)
    {
        var translationCreateResponse = await Client.ExecuteAsync<CreateTranslationDto>(request);

        var requestId = translationCreateResponse.Data.RequestId;
        Client.PollTransaltionOperation(requestId, Creds);

        var resultEndpoint = $"mt/translations/async/{requestId}/content";
        var resultRequest = new LanguageWeaverRequest(resultEndpoint, Method.Get);

        return await Client.GetAsync(resultRequest);
    }

    #endregion
}