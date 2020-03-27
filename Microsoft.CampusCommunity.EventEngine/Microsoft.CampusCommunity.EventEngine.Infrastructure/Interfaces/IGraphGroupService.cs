using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Microsoft.CampusCommunity.EventEngine.Infrastructure.Interfaces
{
    public interface IGraphGroupService
    {
        Task<IEnumerable<Group>> GetEventGroups();
        Task<IEnumerable<Group>> GetGroupsByName(string groupName);
    }
}
