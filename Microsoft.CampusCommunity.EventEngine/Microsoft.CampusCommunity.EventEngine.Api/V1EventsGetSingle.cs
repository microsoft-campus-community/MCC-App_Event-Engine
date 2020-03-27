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
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsGetSingle
    {

        private IGraphEventService _graphEventService;

        public V1EventsGetSingle(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        [FunctionName("V1EventsGetSingle")]
        public async Task<ActionResult<MCCEvent>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/events/{eventId}")] HttpRequest req,
            ILogger log,
            String eventId)
        {
            return new OkObjectResult(_graphEventService.GetEvent(eventId));
            
        }
    }
}
