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


    public class V1PublicEventsGetSingle
    {

        private IGraphEventServicePublic _publicGraphEventService;


        public V1PublicEventsGetSingle(IGraphEventServicePublic publicGraphEventService)
        {
            _publicGraphEventService = publicGraphEventService;

        }
        [FunctionName("V1PublicEventsGetSingle")]
        public async Task<ActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/public/events/{eventId}")] HttpRequest req,
            ILogger log,
            String eventId)
        {
            //return await _publicGraphEventService.GetPublicEvent(eventId);
            return new OkResult();
        }
    }
}
