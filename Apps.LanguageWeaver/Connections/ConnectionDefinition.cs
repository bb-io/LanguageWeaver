using Apps.LanguageWeaver.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.LanguageWeaver.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = new List<ConnectionProperty>()
            {
                new(CredsNames.ClientId) { DisplayName = "Client ID" },
                new(CredsNames.ClientSecret) { DisplayName = "Client secret" }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var apiKey = values.First(v => v.Key == CredsNames.ClientId);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            apiKey.Key,
            apiKey.Value
        );
        var secret = values.First(v => v.Key == CredsNames.ClientSecret);
        yield return new AuthenticationCredentialsProvider(
            AuthenticationCredentialsRequestLocation.None,
            secret.Key,
            secret.Value
        );
    }
}