using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.LanguageWeaver.Connections
{
    public class ConnectionDefinition : IConnectionDefinition
    {

        public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>()
        {
            new ConnectionPropertyGroup
            {
                Name = "Developer API key",
                AuthenticationType = ConnectionAuthenticationType.Undefined,
                ConnectionUsage = ConnectionUsage.Actions,
                ConnectionProperties = new List<ConnectionProperty>()
                {
                    new ConnectionProperty("Client Id"),
                    new ConnectionProperty("Client secret")
                }
            }
        };

        public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(Dictionary<string, string> values)
        {
            var apiKey = values.First(v => v.Key == "Client Id");
            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                apiKey.Key,
                apiKey.Value
            );
            var secret = values.First(v => v.Key == "Client secret");
            yield return new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                secret.Key,
                secret.Value
            );
        }
    }
}
