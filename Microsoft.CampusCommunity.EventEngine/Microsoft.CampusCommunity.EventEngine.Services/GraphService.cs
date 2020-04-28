using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers;
using Microsoft.Graph;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphClientConfiguration _graphClientConfiguration;
       private IConfidentialClientApplication _msalClient;

        public GraphServiceClient Client { get; private set; }

        public GraphService(IOptions<GraphClientConfiguration> graphClientConfiguration)
        {
            _graphClientConfiguration = graphClientConfiguration.Value;
            BuildGraphClient();
        }

        private void BuildGraphClient()
        {
           Client = new GraphServiceClient(new AzureFunctionAuthenticationProvider(_graphClientConfiguration));
        }

    }
}
