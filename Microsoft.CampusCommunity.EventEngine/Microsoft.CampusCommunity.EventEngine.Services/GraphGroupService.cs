using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Services
{
    class GraphGroupService : IGraphGroupService
    {
        private static readonly string EVENTGROUPNAME = "Events";
        private IGraphService _graphService;

        public GraphGroupService(IGraphService graphService)
        {
            _graphService = graphService;
        }


        public async Task<IEnumerable<Group>> GetEventGroups()
        {
            return await GetGroupsByName(EVENTGROUPNAME);
        }

        public async Task<IEnumerable<Group>> GetGroupsByName(string groupName)
        {
          return await _graphService.Client.Groups.Request()
                .Filter("displayName eq " + groupName)
                .GetAsync();
        }
    }
}
