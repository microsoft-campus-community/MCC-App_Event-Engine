using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Security;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers
{
    /// <summary>
    /// A custom IAuthenticationProvider to authenticate against the MS Graph based on the username/password flow with the admin principle.
    /// </summary>
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
               .WithTenantId(_graphClientConfiguration.TenantId)
                  .WithAuthority(new Uri(_graphClientConfiguration.Authority))
                  .Build();
            string[] scopes = new string[] { "Group.ReadWrite.All" };
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
                    //TODO: Figure out how to make the password more secure using securestring here is pointless.
                    var securePassword = new SecureString();
                    foreach (char c in _graphClientConfiguration.AdminPrincipalPassword)
                        securePassword.AppendChar(c); 
                    
                    result = await app.AcquireTokenByUsernamePassword(scopes,
                                                                     _graphClientConfiguration.AdminPrincipalUsername,
                                                                      securePassword)
                                       .ExecuteAsync();
                }
                catch (MsalException ex)
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
