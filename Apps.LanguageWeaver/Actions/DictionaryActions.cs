using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Constants;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Apps.LanguageWeaver.Models.Requests.Dictionary;
using Apps.LanguageWeaver.Models.Responses.Dictionary;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class DictionaryActions : LanguageWeaverInvocable
{
    public DictionaryActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    //[Action("List dictionaries", Description = "List all account dictionaries")]
    //public async Task<ListDictionariesResponse> ListDictionaries()
    //{
    //    var self = await GetSelf();
    //    var endpoint = $"accounts/{self.AccountId}/dictionaries";

    //    var request = new LanguageWeaverRequest(endpoint, Method.Get);

    //    return await Client.ExecuteWithErrorHandling<ListDictionariesResponse>(request);
    //}

    [Action("Get dictionary", Description = "Get specific dictionary")]
    public async Task<DictionaryDto> GetDictionary([ActionParameter] DictionaryRequest input)
    {
        var self = await GetSelf();
        var endpoint = $"accounts/{self.AccountId}/dictionaries/{input.DictionaryId}";

        var request = new LanguageWeaverRequest(endpoint, Method.Get);

        return await Client.ExecuteWithErrorHandling<DictionaryDto>(request);
    }

    [Action("Create dictionary", Description = "Create a new dictionary")]
    public async Task<DictionaryDto> CreateDictionary([ActionParameter] CreateDictionaryRequest input)
    {
        var self = await GetSelf();
        var endpoint = $"accounts/{self.AccountId}/dictionaries";

        var request = new LanguageWeaverRequest(endpoint, Method.Post)
            .WithJsonBody(input, JsonConfig.Settings);

        return await Client.ExecuteWithErrorHandling<DictionaryDto>(request);
    }

    [Action("Delete dictionary", Description = "Delete specific dictionary")]
    public async Task DeleteDictionary([ActionParameter] DictionaryRequest input)
    {
        var self = await GetSelf();
        var endpoint = $"accounts/{self.AccountId}/dictionaries/{input.DictionaryId}";

        var request = new LanguageWeaverRequest(endpoint, Method.Delete);

        await Client.ExecuteWithErrorHandling(request);
    }

    private Task<AccountDto> GetSelf()
        => new AccountActions(InvocationContext).GetSelf();
}