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

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsDelete
    {
        private IGraphEventService _graphEventService;

        public V1EventsDelete(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        [FunctionName("V1EventsDelete")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v1/events/{eventId}")] HttpRequest req,
            ILogger log,
            String eventId)
        {

            _graphEventService.DeleteEvent(eventId);
            return new OkResult();
        }
    }
}
