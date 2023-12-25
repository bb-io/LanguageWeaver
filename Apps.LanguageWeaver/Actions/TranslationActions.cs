using System.Net.Mime;
using System.Text.RegularExpressions;
using Apps.LanguageWeaver.Actions.Base;
using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Constants;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Translation;
using Apps.LanguageWeaver.Models.Responses.Translation;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class TranslationActions : BaseActions
{
    public TranslationActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext, fileManagementClient)
    {
    }

    [Action("Translate text", Description = "Translate a text")]
    public async Task<TranslateTextResponse> TranslateText([ActionParameter] TranslateTextRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);
        request.AddJsonBody(new
        {
            sourceLanguageId = input.SourceLanguage ?? "auto",
            targetLanguageId = input.TargetLanguage,
            input = new[] { input.Text },
            model = input.Model ?? "generic",
            //dictionaries = input.Dictionaries ?? new List<string>(),
            translationMode = input.TranslationMode ?? "quality",
        });

        var (response, status) = await Translate(request);
        var translation = JsonConvert.DeserializeObject<TranslationTextResultDto>(response.Content);

        return new(translation, status);
    }

    [Action("Translate text with QE", Description = "Translate a text while also returning a quality estimation")]
    public async Task<TranslateTextWithQeResponse> TranslateTextWithQe([ActionParameter] TranslateTextWithQeRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);
        request.AddJsonBody(new
        {
            sourceLanguageId = input.SourceLanguage,
            targetLanguageId = input.TargetLanguage,
            input = new[] { input.Text },
            model = input.Model ?? "genericqe",
            //dictionaries = input.Dictionaries ?? new List<string>(),
            translationMode = input.TranslationMode ?? "quality",
            qualityEstimation = 1,
        });

        var (response, status) = await Translate(request);
        var translation = JsonConvert.DeserializeObject<TranslationTextResultDto>(response.Content);

        return new(translation, status);
    }

    [Action("Translate file", Description = "Translate a file")]
    public async Task<TranslateFileResponse> TranslateFile([ActionParameter] TranslateFileRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);

        var fileExtension = Path.GetExtension(input.File.Name);
        var inputFormat = FileInputFormats.All.First(f => f.Extension.Contains(fileExtension)).Name;
        var fileBytes = await ConvertToByteArray(input.File);
        request.AddParameter("sourceLanguageId", input.SourceLanguage ?? "auto");
        request.AddParameter("targetLanguageId", input.TargetLanguage);
        request.AddParameter("model", input.Model ?? "generic");
        request.AddParameter("translationMode", input.TranslationMode ?? "quality");
        request.AddParameter("pdfConverter", input.PdfConverter ?? "STANDARD");
        request.AddParameter("inputFormat", input.InputFormat ?? inputFormat);
        request.AddFile("input", fileBytes, input.FileName ?? input.File.Name);

        var (response, status) = await Translate(request);
        var contentDispositionHeader = response.ContentHeaders.First(header => header.Name == "Content-Disposition");
        var filename = Regex.Match(contentDispositionHeader.Value.ToString(), @"filename=""(.*?)""").Groups[1].Value;

        if (!MimeTypes.TryGetMimeType(filename, out var contentType) || contentType == MediaTypeNames.Text.Plain)
            contentType = MediaTypeNames.Application.Octet;

        var file = await ConvertToFileReference(response.RawBytes ?? Array.Empty<byte>(), filename, contentType);
        
        return new()
        {
            File = file,
            Stats = status.TranslationStats
        };
    }

    [Action("Translate file with QE", Description = "Translate a file while also returning a quality estimation")]
    public async Task<TranslateFileWithQeResponse> TranslateFileWithQe([ActionParameter] TranslateFileWithQeRequest input)
    {
        var endpoint = "mt/translations/async";
        var request = new LanguageWeaverRequest(endpoint, Method.Post);

        var fileExtension = Path.GetExtension(input.File.Name);
        var inputFormat = FileInputFormats.All.First(f => f.Extension.Contains(fileExtension)).Name;
        var fileBytes = await ConvertToByteArray(input.File);
        request.AddParameter("sourceLanguageId", input.SourceLanguage);
        request.AddParameter("targetLanguageId", input.TargetLanguage);
        request.AddParameter("model", input.Model ?? "genericqe");
        request.AddParameter("translationMode", input.TranslationMode ?? "quality");
        request.AddParameter("pdfConverter", input.PdfConverter ?? "STANDARD");
        request.AddParameter("qualityEstimation", 1);
        request.AddParameter("inputFormat", input.InputFormat ?? inputFormat);
        request.AddFile("input", fileBytes, input.FileName ?? input.File.Name);

        var (response, status) = await Translate(request);
        var contentDispositionHeader = response.ContentHeaders.First(header => header.Name == "Content-Disposition");
        var filename = Regex.Match(contentDispositionHeader.Value.ToString(), @"filename=""(.*?)""").Groups[1].Value;

        if (!MimeTypes.TryGetMimeType(filename, out var contentType) || contentType == MediaTypeNames.Text.Plain)
            contentType = MediaTypeNames.Application.Octet;
        
        var file = await ConvertToFileReference(response.RawBytes ?? Array.Empty<byte>(), filename, contentType);

        return new()
        {
            File = file,
            Stats = status.TranslationStats,
            Quality = status.QualityEstimation?.FirstOrDefault()
        };
    }

    private async Task<(RestResponse, TranslationStatusDto)> Translate(RestRequest request)
    {
        var translationCreateResponse = await Client.ExecuteAsync<CreateTranslationDto>(request);

        var requestId = translationCreateResponse.Data.RequestId;
        var status = Client.PollTransaltionOperation(requestId);

        var resultEndpoint = $"mt/translations/async/{requestId}/content";
        var resultRequest = new LanguageWeaverRequest(resultEndpoint, Method.Get);

        return (await Client.GetAsync(resultRequest), status);
    }
}