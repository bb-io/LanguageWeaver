using Apps.LanguageWeaver.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.LanguageWeaver.Invocables;

public class LanguageWeaverInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds
        => InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected LanguageWeaverClient Client { get; }

    public LanguageWeaverInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds);
    }
}