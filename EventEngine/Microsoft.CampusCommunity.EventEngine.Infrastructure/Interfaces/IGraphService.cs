using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    public interface IGraphService
    {
        GraphServiceClient Client { get; }
    }
}
