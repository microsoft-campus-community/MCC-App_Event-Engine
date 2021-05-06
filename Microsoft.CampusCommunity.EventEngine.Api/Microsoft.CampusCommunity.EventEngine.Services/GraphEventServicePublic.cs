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

        public async Task<IEnumerable<PublicMCCEventLite>> GetPublicEvents(bool includePastEvents)
        {
            IEnumerable<Graph.Event> events = await _graphEventService.GetEvents(includePastEvents);
            List<PublicMCCEventLite> publicEvents = new List<PublicMCCEventLite>();
            foreach (var item in events)
            {
                PublicMCCEventLite currentEvent = new PublicMCCEventLite();
                //currentEvent.fromMCCEvent(item);
                publicEvents.Add(currentEvent);
            }
            return publicEvents;
        }

        public async Task<PublicMCCEvent> GetPublicEvent(string eventId)
        {
            Graph.Event graphEvent = await _graphEventService.GetEvent(eventId);
            PublicMCCEvent publicEvent = new PublicMCCEvent();
            //publicEvent.fromMCCEvent(graphEvent);
            return publicEvent;
        }
    }
}
