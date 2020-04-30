using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.Graph;
using Newtonsoft.Json;

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

        public async Task<MCCEvent> CreateEvent(MCCEvent newEvent)
        {
            Event toCreate = newEvent.toEvent();
            Event createdEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request().AddAsync(toCreate);
            Event onlyExtensionDataEvent = new Event();
            onlyExtensionDataEvent.AdditionalData = new Dictionary<string, object>();
            onlyExtensionDataEvent.AdditionalData.Add("extvmri0qlh_eventEngine", newEvent.Extvmri0qlh_eventEngine);
            Event mccEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[createdEvent.Id].Request().UpdateAsync(onlyExtensionDataEvent);
            newEvent.Id = mccEvent.Id;
            /*
            Event createdEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request().AddAsync(newEvent.toEvent());
            Event onlyExtensionDataEvent = new Event();
            onlyExtensionDataEvent.AdditionalData = new Dictionary<String, object>();
            onlyExtensionDataEvent.AdditionalData.Add("extvmri0qlh_eventEngine" ,newEvent.Extvmri0qlh_eventEngine);
            //known issue: https://docs.microsoft.com/en-us/graph/extensibility-overview#schema-extensions need to use PATCH to create schema extension with event

            Console.WriteLine(JsonConvert.SerializeObject(onlyExtensionDataEvent));

            Event mccEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[createdEvent.Id].Request().UpdateAsync(onlyExtensionDataEvent);
            newEvent.fromEvent(mccEvent);
            
    */

            return newEvent;

        }

        public async void DeleteEvent(string eventId)
        {
          await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().DeleteAsync();
        }

        public async Task<MCCEvent> GetEvent(string eventId)
        {
            var query = new List<Option>()
            {
                new QueryOption("$select", "extvmri0qlh_eventEngine"),
            };

            var eventById = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request(query).GetAsync();
            if(eventById != null)
            {
                return new MCCEvent(eventById);
            } else
            {
                throw new NullReferenceException();
            }
            
        }

        public async Task<IEnumerable<MCCEvent>> GetEvents(Boolean includePastEvents)
        {
            List<MCCEvent> results = new List<MCCEvent>();
            var query = new List<Option>()
            {
                new QueryOption("$select", "extvmri0qlh_eventEngine"),
            };
            if (!includePastEvents)
            {
                query.Add(new QueryOption("$filter", "start/dateTime ge '" + DateTime.Now.ToString("yyyy-MM-ddThh:mm") + "'"));
            }
            
            //TODO: How to effectively work with schema extension
            var events = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request(query).GetAsync();

            foreach(Event eventObject in events){
                 results.Add(new MCCEvent(eventObject));
            }
            return results;

        }
    }
}