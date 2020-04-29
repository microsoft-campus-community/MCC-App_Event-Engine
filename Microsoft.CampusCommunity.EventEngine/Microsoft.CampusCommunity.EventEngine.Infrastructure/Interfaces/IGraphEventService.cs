using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    public interface IGraphEventService
    {
        Task<IEnumerable<MCCEvent>> GetEvents(Boolean includePastEvents);

        void DeleteEvent(string eventId);

        Task<MCCEvent> GetEvent(String eventId);

        Task<MCCEvent> CreateEvent(MCCEvent newEvent);
    }
}
