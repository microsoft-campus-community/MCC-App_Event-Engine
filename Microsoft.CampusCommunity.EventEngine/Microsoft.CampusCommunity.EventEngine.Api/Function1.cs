using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Token(
                Identity = TokenIdentityMode.UserFromRequest,
                Resource = "https://graph.microsoft.com",
                IdentityProvider = "AAD"
            )] string graphToken,
            ILogger log)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", graphToken);
            var me = await client.GetStringAsync("https://graph.microsoft.com/v1.0/me/");

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(me, Encoding.UTF8, "application/json")
            };
            /*log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);*/
        }
    }
}
