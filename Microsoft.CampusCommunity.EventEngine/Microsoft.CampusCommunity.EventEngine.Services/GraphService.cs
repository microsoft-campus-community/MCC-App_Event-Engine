using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers;
using Microsoft.Graph;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphClientConfiguration _graphClientConfiguration;
       private IConfidentialClientApplication _msalClient;

        public GraphServiceClient Client { get; private set; }

        public GraphService(IOptions<GraphClientConfiguration> graphClientConfiguration)
        {
            _graphClientConfiguration = graphClientConfiguration.Value;
            BuildGraphClient();
        }

        private void BuildGraphClient()
        {
            string[] scopes = new string[] { "User.Read", "Group.ReadWrite.All" };

            /* Integrated windows authentication Does not work because of: Integrated Windows Auth is not supported for managed users.
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(_graphClientConfiguration.ClientId)
                .WithTenantId(_graphClientConfiguration.TenantId)
                .Build();

            IntegratedWindowsAuthenticationProvider authProvider = new IntegratedWindowsAuthenticationProvider(publicClientApplication, scopes);*/

            // username/password flow does not work because user needs to consent to application which is why interactivity is required
          /* IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(_graphClientConfiguration.ClientId)
                .WithTenantId(_graphClientConfiguration.TenantId)
                .Build();


            UsernamePasswordProvider authProvider = new UsernamePasswordProvider(publicClientApplication, scopes);
            */
            /* client credential provider. Does not work, see https://docs.microsoft.com/en-us/graph/known-issues#groups
             *_msalClient = ConfidentialClientApplicationBuilder.Create(_graphClientConfiguration.ClientId)
                 .WithClientSecret(_graphClientConfiguration.ClientSecret)
                 .WithAuthority(new Uri(_graphClientConfiguration.Authority))
                 .Build();
             var authProvider = new ClientCredentialProvider(_msalClient);*/
           //Client = new GraphServiceClient(authProvider);
           Client = new GraphServiceClient(new AzureFunctionAuthenticationProvider(_graphClientConfiguration));
        }

    }
}
