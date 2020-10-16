using Microsoft.CampusCommunity.EventEngine.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    public interface IGraphEventServicePublic
    {

        /// <summary>
        /// Get all MCC events stored in MS Graph.
        /// </summary>
        /// <param name="includePastEvents">Whether or not to fetch events that started in the past.</param>
        /// <returns>All MCC events that fulfill the filters.</returns>
        Task<IEnumerable<PublicMCCEvent>> GetPublicEvents(Boolean includePastEvents);
    }
}
