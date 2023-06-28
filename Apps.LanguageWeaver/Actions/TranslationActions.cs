using Apps.LanguageWeaver.Dto;
using Apps.LanguageWeaver.Models.Requests;
using Apps.LanguageWeaver.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.LanguageWeaver.Actions
{
    [ActionList]
    public class TranslationActions
    {
        [Action("Translate text", Description = "Translate text")]
        public TranslateTextResponse TranslateText(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] TranslateTextRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
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

        [Action("Translate file", Description = "Translate file")]
        public TranslateFileResponse TranslateFile(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] TranslateFileRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
            var request = new LanguageWeaverRequest("mt/translations/async", Method.Post, authenticationCredentialsProviders);
            request.AddParameter("sourceLanguageId", input.SourceLanguage);
            request.AddParameter("targetLanguageId", input.TargetLanguage);
            request.AddParameter("model", "generic");
            request.AddFile("input", input.File, input.Filename);
            var translationCreateResponse = client.Execute<CreateTranslationDto>(request).Data;
            client.PollTransaltionOperation(translationCreateResponse.RequestId, authenticationCredentialsProviders);
            var resultRequest = new LanguageWeaverRequest($"mt/translations/async/{translationCreateResponse.RequestId}/content", Method.Get, authenticationCredentialsProviders);
            var translatedFile = client.Get(resultRequest).RawBytes;
            return new TranslateFileResponse()
            {
                Filename = input.Filename,
                File = translatedFile
            };
        }

        [Action("Identify text language", Description = "Identify text language")]
        public IdentifyTextLanguageResponse IdentifyTextLanguage(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] IdentifyTextLanguageRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
            var request = new LanguageWeaverRequest("multi-language-identification/async", Method.Post, authenticationCredentialsProviders);
            request.AddJsonBody(new
            {
                input = input.Text
            });
            var identificationCreateResponse = client.Execute<CreateTranslationDto>(request).Data;
            client.PollIndentificationOperation(identificationCreateResponse.RequestId, authenticationCredentialsProviders);
            var resultRequest = new LanguageWeaverRequest($"multi-language-identification/async/{identificationCreateResponse.RequestId}/result", Method.Get, authenticationCredentialsProviders);
            var identifiedResult = client.Get<IdentifyTextLanguageResponse>(resultRequest);
            return identifiedResult;
        }

        [Action("Identify file language", Description = "Identify file language")]
        public IdentifyTextLanguageResponse IdentifyFileLanguage(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
            [ActionParameter] IdentifyFileLanguageRequest input)
        {
            var client = new LanguageWeaverClient(authenticationCredentialsProviders);
            var request = new LanguageWeaverRequest("multi-language-identification/async", Method.Post, authenticationCredentialsProviders);
            request.AddFile("input", input.File, input.Filename);
            var identificationCreateResponse = client.Execute<CreateTranslationDto>(request).Data;
            client.PollIndentificationOperation(identificationCreateResponse.RequestId, authenticationCredentialsProviders);
            var resultRequest = new LanguageWeaverRequest($"multi-language-identification/async/{identificationCreateResponse.RequestId}/result", Method.Get, authenticationCredentialsProviders);
            var identifiedResult = client.Get<IdentifyTextLanguageResponse>(resultRequest);
            return identifiedResult;
        }
    }
}
