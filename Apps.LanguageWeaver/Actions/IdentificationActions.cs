using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Translation;
using Apps.LanguageWeaver.Models.Responses.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Actions
{
    [ActionList]
    public class IdentificationActions : LanguageWeaverInvocable
    {
        public IdentificationActions(InvocationContext invocationContext) : base(invocationContext)
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
        public Task<IdentifyTextLanguageResponse> IdentifyFileLanguage(
            [ActionParameter] IdentifyFileLanguageRequest input)
        {
            var endpoint = "multi-language-identification/async";
            var request = new LanguageWeaverRequest(endpoint, Method.Post)
                .AddFile("input", input.File.Bytes, input.FileName ?? input.File.Name)
                .AddParameter("inputFormat", input.Format ?? "PLAIN");

            return IdentifyLanguage(request);
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
