using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using System.Web.Http;

namespace Microsoft.CampusCommunity.EventEngine.Api
{
    public class V1EventsPatch
    {
        private IGraphEventService _graphEventService;

        public V1EventsPatch(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        [FunctionName("V1EventsPatch")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "v1/events/{eventId}")] HttpRequest req,
            ILogger log,
            String eventId)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var inputEvent = JsonConvert.DeserializeObject<MCCEvent>(requestBody);


            try
            {
                
                inputEvent.SerializeEventSchemaExtension = true;
                MCCEvent updatedEvent = await _graphEventService.UpdateEvent(eventId, inputEvent);
                if (updatedEvent.Id != null)
                {
                    Uri uriToEvent = new Uri($"{req.Path}/{updatedEvent.Id}", UriKind.Relative);
                    return new Microsoft.AspNetCore.Mvc.CreatedResult(uriToEvent, updatedEvent);
                }
                else
                {
                    return new BadRequestObjectResult(inputEvent);
                }

            }
            catch (Exception ex)
            {

                log.LogError(ex, "Exception occured while executing PATCH v1/events with event {event}.", JsonConvert.SerializeObject(inputEvent));
                return new InternalServerErrorResult();

            }
        }
    }
}
