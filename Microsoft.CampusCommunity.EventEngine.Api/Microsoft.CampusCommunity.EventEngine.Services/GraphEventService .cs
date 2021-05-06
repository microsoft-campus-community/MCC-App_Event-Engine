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

        public async Task<Graph.Event> CreateEvent(CommastoEvent newEvent)
        {


            Graph.Event createdEvent = await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request().AddAsync(newEvent.ToGraphEvent());
          
            return await UpdateSchemaExtension(createdEvent.Id, newEvent.GetEventSchemaExtension());
           

        }

        public async void DeleteEvent(string eventId)
        {
            await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().DeleteAsync();
        }

        public async Task<Graph.Event> GetEvent(string eventId)
        {
             var query = new List<Option>()
            {
                new QueryOption("$select", IGraphEventService.EVENTSCHEMAEXTENSIONID),
            };

           return await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request(query).GetAsync();
            
        }

        private async Task<Graph.Event> UpdateSchemaExtension(string eventId, IEventSchemaExtension schemaExtensionUpdate)
        {
            if (schemaExtensionUpdate != null)
            {
                Graph.Event toPatch = new Graph.Event();
                toPatch.AdditionalData = new Dictionary<string, object>();
                toPatch.AdditionalData.Add(IGraphEventService.EVENTSCHEMAEXTENSIONID, schemaExtensionUpdate);
                return await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().UpdateAsync(toPatch);
            }
            return new Graph.Event();
        }

        public async Task<Graph.Event> UpdateEvent(string eventId, CommastoEvent updatedEvent)
        {
             await UpdateSchemaExtension(eventId, updatedEvent.GetEventSchemaExtension());
            
            return await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events[eventId].Request().UpdateAsync(updatedEvent.ToGraphEvent());
        }


        public async Task<IEnumerable<Graph.Event>> GetEvents(Boolean includePastEvents)
        {
            
            var query = new List<Option>()
            {
                new QueryOption("$select", IGraphEventService.EVENTSCHEMAEXTENSIONID),
            };
            if (!includePastEvents)
            {
                query.Add(new QueryOption("$filter", "start/dateTime ge '" + DateTime.Now.ToString("yyyy-MM-ddThh:mm") + "'"));
            }

            return await _graphService.Client.Groups[GraphEventService.EVENTGROUPID].Events.Request(query).GetAsync();


        }
    }
}
