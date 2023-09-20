using Apps.LanguageWeaver.Actions;
using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Responses.Dictionary;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageWeaver.DataSourceHandlers;

public class DictionaryDataHandler : LanguageWeaverInvocable, IAsyncDataSourceHandler
{
    public DictionaryDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
        CancellationToken cancellationToken)
    {
        var self = await new AccountActions(InvocationContext).GetSelf();
        var endpoint = $"accounts/{self.AccountId}/dictionaries";

        var request = new LanguageWeaverRequest(endpoint, Method.Get, Creds);
        var response = await Client.ExecuteWithErrorHandling<ListDictionariesResponse>(request);

        return response.Dictionaries
            .Where(x => context.SearchString is null ||
                        x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreationDate)
            .ToDictionary(x => x.DictionaryId, x => x.Name);
    }
}