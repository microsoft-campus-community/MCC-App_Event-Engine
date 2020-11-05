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
using System.Web.Http;
using System.Net;
using System.Net.Http;

using System.Reflection;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsPost
    {
        private IGraphEventService _graphEventService;

        public V1EventsPost(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        [FunctionName("V1EventsPost")]
        public async Task<ActionResult<MCCEvent>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/events")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var inputEvent = JsonConvert.DeserializeObject<IEvent>(requestBody);


            try
            {
                MCCEvent createdEvent = await _graphEventService.CreateEvent(inputEvent);
                if (createdEvent.Id != null)
                {
                    Uri uriToEvent = new Uri($"{req.Path}/{createdEvent.Id}", UriKind.Relative);
                    return new Microsoft.AspNetCore.Mvc.CreatedResult(uriToEvent, createdEvent);
                }
                else
                {
                    return new BadRequestObjectResult(inputEvent);
                }

            }
            catch (Exception ex)
            {

                log.LogError(ex, "Exception occured while executing POST v1/events with event {event}.", JsonConvert.SerializeObject(inputEvent));
                return new InternalServerErrorResult();

            }

        }

    }
}
