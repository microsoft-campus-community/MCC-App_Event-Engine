using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    class GraphService : IGraphService
    {
        private readonly GraphClientConfiguration _configuration;
        private IConfidentialClientApplication _msalClient;
        public GraphServiceClient Client { get; private set; }

        public GraphService(GraphClientConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void BuildGraphClient()
        {
            _msalClient = ConfidentialClientApplicationBuilder.Create(_configuration.ClientId)
                .WithClientSecret(_configuration.ClientSecret)
                .WithAuthority(new Uri(_configuration.Authority))
                .Build();
            ClientCredentialProvider clientCredentialProvider = new ClientCredentialProvider(_msalClient);
            Client = new GraphServiceClient(clientCredentialProvider);
        }

    }
}
