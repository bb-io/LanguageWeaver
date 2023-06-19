using Apps.LanguageWeaver.Dto;
using Apps.LanguageWeaver.Models.Requests;
using Apps.LanguageWeaver.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.LanguageWeaver.Actions
{
    [ActionList]
    public class TranslationActions
    {
        [Action("Translate text", Description = "Translate text")]
        public TranslateTextResponse TranslateText(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] TranslateTextRequest input)
        {
            var client = new LanguageWeaverClient();
            var request = new LanguageWeaverRequest("mt/translations/async", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {            
                sourceLanguageId = input.SourceLanguage,
                targetLanguageId = input.TargetLanguage,
                input = new[] { input.Text },
                model = "generic"
            });
            var translationCreateResponse = client.Execute<CreateTranslationDto>(request).Data;
            client.PollTransaltionOperation(translationCreateResponse.RequestId, authenticationCredentialsProviders);
            var resultRequest = new LanguageWeaverRequest($"mt/translations/async/{translationCreateResponse.RequestId}/content", Method.Get, authenticationCredentialsProviders);
            var translation = client.Get<TranslationTextResultDto>(resultRequest);
            return new TranslateTextResponse(translation);
        }
    }
}
