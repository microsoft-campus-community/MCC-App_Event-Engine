using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Linq;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers
{
    public class AzureFunctionAuthenticationProvider : IAuthenticationProvider
    {
        private readonly GraphClientConfiguration _graphClientConfiguration;


        public AzureFunctionAuthenticationProvider(GraphClientConfiguration graphClientConfig)

        {
            _graphClientConfiguration = graphClientConfig;

        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            IPublicClientApplication app = PublicClientApplicationBuilder.Create(_graphClientConfiguration.ClientId)
                  .WithAuthority(_graphClientConfiguration.Authority)
                  .Build();
            string[] scopes = new string[] { "user.read" };
            var accounts = await app.GetAccountsAsync();

            AuthenticationResult result = null;
            if (accounts.Any())
            {
                result = await app.AcquireTokenSilent(scopes, accounts.FirstOrDefault())
                                  .ExecuteAsync();
            }
            else
            {
                try
                {
                    result = await app.AcquireTokenByUsernamePassword(scopes,
                                                                     _graphClientConfiguration.AdminPrincipalUsername,
                                                                      _graphClientConfiguration.AdminPrincipalPassword)
                                       .ExecuteAsync();
                }
                catch (MsalException)
                {
                    // See details below
                }
            }

            if(result != null)
            {
                request.Headers.Add("Authorization", result.CreateAuthorizationHeader());
            }
        }
    }
}
