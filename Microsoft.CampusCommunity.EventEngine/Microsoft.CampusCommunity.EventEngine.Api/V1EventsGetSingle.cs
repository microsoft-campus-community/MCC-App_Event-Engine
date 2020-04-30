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
            try
            {
                 return new OkObjectResult(await _graphEventService.GetEvent(eventId));
            } catch(NullReferenceException)
            {
                return new NotFoundObjectResult(eventId);
            } catch(Microsoft.Graph.ServiceException ex)
            {
                log.LogError(ex, "Microsoft Graph exception occured while executing v1/events/{eventId}.", eventId);
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new NotFoundObjectResult(eventId);
                } else
                {
                    return new ExceptionResult(ex, false);
                }
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType());
                log.LogError(ex, "Exception occured while executing v1/events/{eventId}.", eventId);
                return new InternalServerErrorResult();
            }
            
        }
    }
}
