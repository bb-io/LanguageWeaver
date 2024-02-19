using Apps.LanguageWeaver.Actions.Base;
using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Translation;
using Apps.LanguageWeaver.Models.Responses.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.LanguageWeaver.Actions
{
    [ActionList]
    public class IdentificationActions : BaseActions
    {
        public IdentificationActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
            : base(invocationContext, fileManagementClient)
        {
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
        public async Task<IdentifyTextLanguageResponse> IdentifyFileLanguage(
            [ActionParameter] IdentifyFileLanguageRequest input)
        {
            var endpoint = "multi-language-identification/async";
            var fileBytes = await ConvertToByteArray(input.File);
            var request = new LanguageWeaverRequest(endpoint, Method.Post)
                .AddFile("input", fileBytes, input.FileName ?? input.File.Name)
                .AddParameter("inputFormat", input.Format ?? "PLAIN");

            return await IdentifyLanguage(request);
        }

        private async Task<IdentifyTextLanguageResponse> IdentifyLanguage(RestRequest request)
        {
            var identificationCreateResponse = await Client.ExecuteAsync<CreateTranslationDto>(request);

            var requestId = identificationCreateResponse.Data.RequestId;
            Client.PollIndentificationOperation(requestId, Creds);

            var resultEndpoint = $"multi-language-identification/async/{requestId}/result";
            var resultRequest = new LanguageWeaverRequest(resultEndpoint, Method.Get);

            return await Client.GetAsync<IdentifyTextLanguageResponse>(resultRequest);
        }
    }
}
