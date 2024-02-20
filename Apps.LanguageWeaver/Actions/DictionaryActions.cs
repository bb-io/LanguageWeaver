using System.Net.Mime;
using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Constants;
using Apps.LanguageWeaver.Extensions;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Dictionary;
using Apps.LanguageWeaver.Models.Responses.Dictionary;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class DictionaryActions : LanguageWeaverInvocable
{
    private readonly IFileManagementClient _fileManagementClient;
    private readonly string _accountId;
    
    public DictionaryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
        _accountId = GetSelf().Result.AccountId;
    }
    
    [Action("Get dictionary", Description = "Get specific dictionary")]
    public async Task<DictionaryDto> GetDictionary([ActionParameter] DictionaryRequest input)
    {
        var endpoint = $"accounts/{_accountId}/dictionaries/{input.DictionaryId}";
        var request = new LanguageWeaverRequest(endpoint, Method.Get);

        return await Client.ExecuteWithErrorHandling<DictionaryDto>(request);
    }

    [Action("Create dictionary", Description = "Create a new dictionary")]
    public async Task<DictionaryDto> CreateDictionary([ActionParameter] CreateDictionaryRequest input)
    {
        var endpoint = $"accounts/{_accountId}/dictionaries";
        var request = new LanguageWeaverRequest(endpoint, Method.Post).WithJsonBody(input, JsonConfig.Settings);

        return await Client.ExecuteWithErrorHandling<DictionaryDto>(request);
    }

    [Action("Delete dictionary", Description = "Delete specific dictionary")]
    public async Task DeleteDictionary([ActionParameter] DictionaryRequest input)
    {
        var endpoint = $"accounts/{_accountId}/dictionaries/{input.DictionaryId}";
        var request = new LanguageWeaverRequest(endpoint, Method.Delete);

        await Client.ExecuteWithErrorHandling(request);
    }

    [Display("Import glossary", Description = "Import a glossary into an existing or new dictionary")]
    public async Task<DictionaryDto> ImportGlossary([ActionParameter] ImportGlossaryRequest input)
    {
        if (input.DictionaryId == null && (input.Source == null || input.Target == null))
            throw new Exception(
                "Source and target languages must be specified when importing glossary into a new dictionary.");
        
        DictionaryDto dictionary;
        IEnumerable<DictionaryTermDto> dictionaryTerms;
        
        await using var glossaryStream = await _fileManagementClient.DownloadAsync(input.Glossary);
        var glossary = await glossaryStream.ConvertFromTbx();

        if (input.DictionaryId == null)
        {
            var dictionaries = await ListDictionaries();
            var dictionaryName = input.DictionaryTitle ??
                                 glossary.Title ?? $"Glossary import {DateTime.Now.ToString("f")}";
            dictionaryName =
                dictionaries.Any(dictionary =>
                    dictionary.Name.Equals(dictionaryName, StringComparison.OrdinalIgnoreCase))
                    ? $"{dictionaryName} {DateTime.Now.ToString("f")}"
                    : dictionaryName;
            
            dictionary = await CreateDictionary(new CreateDictionaryRequest
            {
                Name = dictionaryName,
                Source = input.Source!,
                Target = input.Target!,
                Description = input.DictionaryDescription ?? glossary.SourceDescription
            });
            
            dictionaryTerms = Enumerable.Empty<DictionaryTermDto>();
        }
        else
        {
            dictionary = await GetDictionary(new DictionaryRequest { DictionaryId = input.DictionaryId });

            dictionaryTerms = input.OverwriteDuplicates == true
                ? Enumerable.Empty<DictionaryTermDto>()
                : await ListDictionaryTerms(input.DictionaryId);
        }

        await using var excelStream = glossary.ToLanguageWeaverExcelGlossary(dictionary, dictionaryTerms, 
            input.OverwriteDuplicates ?? false);
        
        var excelBytes = await excelStream.GetByteData();

        var importDictionaryRequest =
            new LanguageWeaverRequest($"accounts/{_accountId}/dictionaries/{dictionary.DictionaryId}/terms",
                Method.Post);
        importDictionaryRequest.AddFile("content", excelBytes, "glossary.xlsx");
        importDictionaryRequest.AddParameter("overwriteDuplicates", input.OverwriteDuplicates ?? false);
        
        await Client.ExecuteWithErrorHandling(importDictionaryRequest);
        return dictionary;
    }

    [Display("Export glossary", Description = "Export a dictionary")]
    public async Task<GlossaryResponse> ExportGlossary([ActionParameter] DictionaryRequest dictionaryInput,
        [ActionParameter] ExportGlossaryRequest input)
    {
        var dictionary = await GetDictionary(dictionaryInput);

        var exportDictionaryRequest =
            new LanguageWeaverRequest($"/accounts/{_accountId}/dictionaries/{dictionary.DictionaryId}/terms/export",
                Method.Get);
        exportDictionaryRequest.AddHeader("Accept", "*/*");

        var exportDictionaryResponse = await Client.ExecuteWithErrorHandling(exportDictionaryRequest);

        await using var excelStream = new MemoryStream(exportDictionaryResponse.RawBytes!);
        var glossary = excelStream.ToGlossary(dictionary, input.Title ?? dictionary.Name,
            input.SourceDescription ?? dictionary.Description);

        await using var glossaryStream = glossary.ConvertToTbx();
        var glossaryFileReference = await _fileManagementClient.UploadAsync(glossaryStream, MediaTypeNames.Text.Xml,
            $"{(glossary.Title ?? dictionary.Name).Replace(' ', '_')}.tbx");

        return new(glossaryFileReference);
    }

    private Task<AccountDto> GetSelf() => new AccountActions(InvocationContext).GetSelf();

    private async Task<IEnumerable<DictionaryDto>> ListDictionaries()
    {
        var request = new LanguageWeaverRequest($"accounts/{_accountId}/dictionaries", Method.Get);
        var response = await Client.ExecuteWithErrorHandling<ListDictionariesResponse>(request);
        return response.Dictionaries;
    }

    private async Task<IEnumerable<DictionaryTermDto>> ListDictionaryTerms(string dictionaryId)
    {
        const int pageSize = 100;
        var endpoint = $"accounts/{_accountId}/dictionaries/{dictionaryId}/terms?pageSize={pageSize}&pageNumber={{0}}";

        var terms = new List<DictionaryTermDto>();
        var pageNumber = 1;
        ListDictionaryTermsResponse response;

        do
        {
            var request = new LanguageWeaverRequest(string.Format(endpoint, pageNumber++), Method.Get);
            response = await Client.ExecuteWithErrorHandling<ListDictionaryTermsResponse>(request);
            terms.AddRange(response.Terms);
        } while (response.Terms.Count() == pageSize);

        return terms;
    }
}