using Apps.LanguageWeaver.Api;
using Apps.LanguageWeaver.Invocables;
using Apps.LanguageWeaver.Models.Dto;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.LanguageWeaver.Actions;

[ActionList]
public class AccountActions : LanguageWeaverInvocable
{
    public AccountActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
    
    //[Action("Get API credentials self", Description = "Retrieve information about the credentials account")]
    public Task<AccountDto> GetSelf()
    {
        var endpoint = "accounts/api-credentials/self";
        var request = new LanguageWeaverRequest(endpoint, Method.Get);
        
        return Client.ExecuteWithErrorHandling<AccountDto>(request);
    }
}