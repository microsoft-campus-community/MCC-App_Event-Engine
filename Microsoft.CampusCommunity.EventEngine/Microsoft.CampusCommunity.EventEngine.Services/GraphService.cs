using Microsoft.CampusCommunity.EventEngine.Infrastructure.Configuration;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Helpers;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphClientConfiguration _graphClientConfiguration;

        public GraphServiceClient Client { get; private set; }

        public GraphService(GraphClientConfiguration graphClientConfiguration)
        {
            _graphClientConfiguration = graphClientConfiguration;
            BuildGraphClient();
        }

        private void BuildGraphClient()
        {
            Client = new GraphServiceClient(new AzureFunctionAuthenticationProvider(_graphClientConfiguration));
        }

    }
}
