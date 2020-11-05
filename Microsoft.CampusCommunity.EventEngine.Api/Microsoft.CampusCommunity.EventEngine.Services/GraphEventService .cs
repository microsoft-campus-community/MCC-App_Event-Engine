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
    /// <summary>
    /// This service provides basic CRUD operations for events stored in MS Graph.
    /// </summary>
    public class GraphEventService : IGraphEventService
    {
        private static readonly string EVENTGROUPID = "7a77d007-2cd2-4cb5-bb71-5ff85aa92e82";
        private IGraphService _graphService;

        public GraphEventService(IGraphService graphService)
        {
            _graphService = graphService;
        }

        public async Task<MCCEvent> CreateEvent(IEvent newEvent)
        {
            newEvent.SerializeMccEventSpecificData = false;
            newEvent.SerializeEventSchemaExtension = false;

            var graphEvent = new Event()
            {
                Subject = newEvent.Subject,
                Body = new ItemBody() {
                    Content = newEvent.Body
                },
                BodyPreview = newEvent.BodyPreview,


            }

            Event createdEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request().AddAsync(newEvent);
            if (newEvent.EventSchemaExtensionData != null)
            {
                newEvent.SerializeEventSchemaExtension = true;
                return await this.UpdateEvent(createdEvent.Id, newEvent);

            }
            else
            {
                newEvent.Id = createdEvent.Id;
                return newEvent;
            }


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
            if (eventById != null)
            {
                return new MCCEvent(eventById);
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public async Task<MCCEvent> UpdateEvent(string eventId, MCCEvent eventWithChangedPropertiesOnly)
        {
            Event toPatch;
            if (eventWithChangedPropertiesOnly.SerializeEventSchemaExtension && eventWithChangedPropertiesOnly.EventSchemaExtensionData != null)
            {
                //Schema extension properties need to be updated wihtout changing anything else.
                toPatch = new Event();
                toPatch.AdditionalData = new Dictionary<string, object>();
                toPatch.AdditionalData.Add("extvmri0qlh_eventEngine", eventWithChangedPropertiesOnly.EventSchemaExtensionData);
                await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().UpdateAsync(toPatch);
            }
            //Update all properties except the schema extension.
            toPatch = eventWithChangedPropertiesOnly.toEvent();
            return new MCCEvent(await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().UpdateAsync(toPatch));
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

            var events = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request(query).GetAsync();

            foreach (Event eventObject in events)
            {
                results.Add(new MCCEvent(eventObject));
            }
            return results;

        }
    }
}
