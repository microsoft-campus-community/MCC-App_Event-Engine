using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers;
using Microsoft.Graph.Auth;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using System.Security;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1ProfileGet
    {
        private IGraphService _graphService;

        private readonly GraphClientConfiguration _graphClientConfiguration;


        public V1ProfileGet(IGraphService graphService, IOptions<GraphClientConfiguration> graphClientConfig)
        {
            _graphService = graphService;
            _graphClientConfiguration = graphClientConfig.Value;
        }

        [FunctionName("V1ProfileGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "v1/profiles")] HttpRequest req,
            /*[Token(
                Identity = TokenIdentityMode.UserFromRequest,
                Resource = "https://graph.microsoft.com",
                IdentityProvider = "AAD"
            )] string graphToken,*/
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            /*var authProvider = new AzureFunctionAuthenticationProviderToken(graphToken);
             GraphServiceClient graphServiceClient = new GraphServiceClient(authProvider);
            Graph.User user = await graphServiceClient.Me.Request().GetAsync();*/

            //TODO: Figure out how to make the password more secure using securestring here is pointless.
            var securePassword = new SecureString();
            foreach (char c in _graphClientConfiguration.AdminPrincipalPassword)
                securePassword.AppendChar(c);

            Graph.User user = await _graphService.Client.Me.Request().WithUsernamePassword(_graphClientConfiguration.AdminPrincipalUsername, securePassword).GetAsync();

            return new OkObjectResult(user);
        }
    }
}
