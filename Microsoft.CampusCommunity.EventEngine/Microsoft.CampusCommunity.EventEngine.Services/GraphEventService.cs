using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphEventService : IGraphEventService
    {
        private static readonly string EVENTGROUPID = "7a77d007-2cd2-4cb5-bb71-5ff85aa92e82";
        private IGraphService _graphService;

        public GraphEventService(IGraphService graphService)
        {
            _graphService = graphService;
        }

        public async Task<IEnumerable<MCCEvent>> GetEvents(Boolean includePastEvents)
        {
            List<MCCEvent> results = new List<MCCEvent>();
            var query = new List<Option>()
            {
                new QueryOption("$select", "ext0bhczrqa_userProfile"),
            };
            if (!includePastEvents)
            {
                query.Add(new QueryOption("$filter", "start / dateTime ge " + DateTime.Now.ToString("yyyy-MM-ddThh:mm")));
            }
            
            var events = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request(query).GetAsync();
            foreach(MCCEvent eventObject in events){
                Object additionalDataEvent = null;
                eventObject.AdditionalData.TryGetValue("extvmri0qlh_eventEngine", out additionalDataEvent);
                eventObject.extvmri0qlh_eventEngine = (IEventSchemaExtension)additionalDataEvent;
                results.Add(eventObject);
            }
            return results;
            

        }
    }
}