using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    public interface IGraphService
    {
        GraphServiceClient Client { get; }

    }
}
