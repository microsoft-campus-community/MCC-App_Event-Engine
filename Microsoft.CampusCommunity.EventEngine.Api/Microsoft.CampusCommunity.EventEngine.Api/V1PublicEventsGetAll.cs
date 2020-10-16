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
using System.Collections.Generic;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Microsoft.CampusCommunity.EventEngine.Api
{

   

    public class V1PublicEventsGetAll
    {


        private IGraphEventServicePublic _publicGraphEventService;


        public V1PublicEventsGetAll(IGraphEventServicePublic publicGraphEventService)
        {
            _publicGraphEventService = publicGraphEventService;

        }
        [FunctionName("V1PublicEventsGetAll")]
        public async Task<ActionResult<IEnumerable<PublicMCCEvent>>> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/public/events")] HttpRequest req,
            ILogger log)
        {
            IEnumerable<PublicMCCEvent> events = await _publicGraphEventService.GetPublicEvents(true);

            return new OkObjectResult(events);
        }
    }
}
