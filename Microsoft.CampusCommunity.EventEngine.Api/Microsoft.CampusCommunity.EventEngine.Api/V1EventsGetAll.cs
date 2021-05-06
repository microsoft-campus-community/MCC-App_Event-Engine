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
using System.Collections.Generic;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsGetAll
    {
        private IGraphEventService _graphEventService;

        public V1EventsGetAll(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        [FunctionName("V1EventsGetAll")]
        public async Task<ActionResult<IEnumerable<Event>>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/events")] HttpRequest req,
            ILogger log)
        {
            string includePastEventsString = null;
            Boolean includePastEvents = false;

            if (req.GetQueryParameterDictionary().TryGetValue("includePastEvents", out includePastEventsString))
            {
                if (!Boolean.TryParse(includePastEventsString, out includePastEvents))
                {
                    includePastEvents = false;
                }
            }

            IEnumerable<Event> events = await _graphEventService.GetEvents(includePastEvents);
            return new OkObjectResult(events);

           
        }
    }
}
