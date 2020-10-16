using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphEventServicePublic: IGraphEventServicePublic
    {

        private IGraphEventService _graphEventService;

        public GraphEventServicePublic(IGraphEventService graphEventService)
        {
            _graphEventService = graphEventService;
        }

        public async Task<IEnumerable<PublicMCCEvent>> GetPublicEvents(bool includePastEvents)
        {
            IEnumerable<MCCEvent> events = await _graphEventService.GetEvents(includePastEvents);
            List<PublicMCCEvent> publicEvents = new List<PublicMCCEvent>();
            foreach (var item in events)
            {
                PublicMCCEvent currentEvent = new PublicMCCEvent();
                currentEvent.fromMCCEvent(item);
                publicEvents.Add(currentEvent);
            }
            return publicEvents;
        }
    }
}
