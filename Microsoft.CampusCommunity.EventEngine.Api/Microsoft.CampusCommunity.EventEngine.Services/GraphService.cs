using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers;
using Microsoft.Graph;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using Microsoft.Graph.Auth;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphClientConfiguration _graphClientConfiguration;

        public GraphServiceClient Client { get; private set; }

        public GraphService(IOptions<GraphClientConfiguration> graphClientConfiguration)
        {
            _graphClientConfiguration = graphClientConfiguration.Value;
            BuildGraphClient();
        }

        /// <summary>
        /// Create a new MS Graph client with username/password based on configuration file.
        /// </summary>
        private void BuildGraphClient()
        {
            Client = new GraphServiceClient(new AzureFunctionAuthenticationProvider(_graphClientConfiguration));
        }

    }
}
