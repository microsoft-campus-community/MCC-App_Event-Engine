using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Graph;
using Microsoft.CampusCommunity.EventEngine.Api.Helpers;
using System.Net.Http;
using System.Net;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public static class V1EventsGet
    {
        [FunctionName("V1EventsGet")]
        public static async Task<ActionResult<User>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/events")] HttpRequestMessage req,
            [Token(
                Identity = TokenIdentityMode.UserFromRequest,
                Resource = "https://graph.microsoft.com",
                IdentityProvider = "AAD"
            )] string graphToken,
            ILogger log)
        {
            GraphServiceClient graphServiceClient = new GraphServiceClient(new AzureFunctionAuthenticationProvider(graphToken));
            User user = await graphServiceClient.Me.Request().GetAsync();
            return user;
            // HttpResponseMessage httpResponseMessage = req.CreateResponse<User>(HttpStatusCode.OK, user);
            //return httpResponseMessage;
        }
    }
}
