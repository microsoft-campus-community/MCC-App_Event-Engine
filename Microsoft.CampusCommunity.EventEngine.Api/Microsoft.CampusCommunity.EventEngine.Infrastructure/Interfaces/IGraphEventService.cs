using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    /// <summary>
    /// Provides CRUD operations for MCC events stored in the MS Graph.
    /// </summary>
    public interface IGraphEventService
    {
        /// <summary>
        /// Get all MCC events stored in MS Graph.
        /// </summary>
        /// <param name="includePastEvents">Whether or not to fetch events that started in the past.</param>
        /// <returns>All MCC events that fulfill the filters.</returns>
        Task<IEnumerable<MCCEvent>> GetEvents(Boolean includePastEvents);

        /// <summary>
        /// Delete a MCC event from MS Graph.
        /// </summary>
        /// <param name="eventId">Id of the event to delete.</param>
        void DeleteEvent(string eventId);

        /// <summary>
        /// Get the object corresponding to a single event.
        /// </summary>
        /// <param name="eventId">Is of the event to get.</param>
        /// <returns>The <c>MCCEvent</c> with matching <c>eventId</c>.</returns>
        Task<MCCEvent> GetEvent(String eventId);

        /// <summary>
        /// Creates a new MCC event in MS Graph.
        /// </summary>
        /// <param name="newEvent">The event to create.</param>
        /// <returns>The event that was created.</returns>
        Task<MCCEvent> CreateEvent(IEvent newEvent);

        /// <summary>
        /// Update properties of an existing MCC event.
        /// </summary>
        /// <param name="eventId">Id of the event to update.</param>
        /// <param name="eventWithChangedPropertiesOnly">An event that contains only the properties that are supposed to change.</param>
        /// <returns>An event containing all the updated properties.</returns>
        Task<MCCEvent> UpdateEvent(string eventId, MCCEvent eventWithChangedPropertiesOnly);

    }
}
