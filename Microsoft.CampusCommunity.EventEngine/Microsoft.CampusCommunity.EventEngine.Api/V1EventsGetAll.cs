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
using Microsoft.Graph;
using Microsoft.Extensions.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsGetAll
    {
        private IGraphEventService _graphEventService;
        private GraphClientConfiguration _graphClientConfiguration;

        public V1EventsGetAll(IGraphEventService graphEventService, GraphClientConfiguration graphClientConfiguration)
        {
            _graphEventService = graphEventService;
            _graphClientConfiguration = graphClientConfiguration;
        }

        [FunctionName("V1EventsGetAll")]
        public async Task<ActionResult<IEnumerable<Event>>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/events")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Hello world");
            log.LogInformation(_graphClientConfiguration.ClientId);
            string includePastEventsString = null;
            Boolean includePastEvents = false;

            if(req.GetQueryParameterDictionary().TryGetValue("includePastEvents", out includePastEventsString)){
                if(!Boolean.TryParse(includePastEventsString, out includePastEvents))
                {
                    includePastEvents = false;
                }
            }
            IEnumerable<Event> events = await _graphEventService.GetEvents(includePastEvents);
            return new OkObjectResult(events);




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
