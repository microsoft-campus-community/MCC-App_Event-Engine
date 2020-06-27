using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.CampusCommunity.Infrastructure.Configuration;
using Microsoft.CampusCommunity.Infrastructure.Interfaces;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Microsoft.CampusCommunity.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMccAuthorizationService _authorizationService;
        private readonly IGraphEventService _graphEventService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="authorizationService"></param>
        public EventsController( IMccAuthorizationService authorizationService, IGraphEventService graphEventService)
        {
            _authorizationService = authorizationService;
            _graphEventService = graphEventService;
        }

        /// <summary>
        /// Get all MCC users. This will only return users where the "location" tag is not empty.
        /// Requirement: <see cref="PolicyNames.Community"/>
        /// </summary>
        /// <param name="scope">User scope</param>
        /// <returns>A user</returns>
        /// <response code="200">Request successful</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        //[Authorize(Policy = PolicyNames.Community)]
        public async Task<ActionResult<IEnumerable<Microsoft.CampusCommunity.Infrastructure.Entities.Dto.SimpleEvent>>> Get(
            [FromQuery(Name = "includePastEvents")] Boolean includePastEvents = false)
        {
            IEnumerable<MCCEvent> events = await _graphEventService.GetEvents(includePastEvents);
            
            var simpleEvents = events.Select(e => new Microsoft.CampusCommunity.Infrastructure.Entities.Dto.SimpleEvent() { Subject = e.Subject, BodyPreview = e.BodyPreview});

            return new OkObjectResult(simpleEvents);
        }

    }
}
