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
    }
}
