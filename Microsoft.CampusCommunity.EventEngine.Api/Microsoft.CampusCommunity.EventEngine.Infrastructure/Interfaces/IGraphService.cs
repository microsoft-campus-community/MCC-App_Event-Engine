using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    /// <summary>
    /// Provides an abstraction for accessing MS Graph.
    /// </summary>
    public interface IGraphService
    {
        GraphServiceClient Client { get; }

    }
}
